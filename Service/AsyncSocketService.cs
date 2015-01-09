using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Service
{
    // 这个对象只是将用到的四个变量包装一下
    public class StateObject
    {
        public Socket workSocket = null;
        public const int BufferSize = 1024;
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder sb = new StringBuilder();
    }
    /// <summary>
    /// 异步
    /// </summary>
    public class AsyncSocketService:IDisposable
    {
        Socket sSocket;
        Socket serverSocket;
        public AsyncSocketService(string hostIP, int port, int listenNum = 0)
        {
            IPAddress ipAddress = IPAddress.Parse(hostIP);
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);
            sSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sSocket.Bind(ipEndPoint);
            sSocket.Listen(listenNum);
            Console.WriteLine("服务已启动,等待连接...");
            
        }

        // 控制线程的的对象
        public static ManualResetEvent allDone = new ManualResetEvent(false);

        public static void StartListening()
        {
            byte[] bytes = new Byte[1024];
            // 这个应该很熟了，和同步的一样
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);
            Socket listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (true)
                {
                    // Set the event to nonsignaled state.
                    allDone.Reset();
                    Console.WriteLine("Waiting for a connection...");
                    //不同的地方开始了。这里采用的接收连接的方法不同
                    //就像上面说的，我想在BeginAccept方法接收连接之后执行AcceptCallback函数
                    //这里只是定义，程序到这还会继续往下执行，一直到allDone.WaitOne();
                    listener.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        listener);

                    // 这句代码将使程序不能继续往下执行，直到 allDone.Set();执行
                    //
                    allDone.WaitOne();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }

        public static void AcceptCallback(IAsyncResult ar)
        {
            // 当接收连接后，按照前面的定义会执行该函数，首先就是让主线程继续往下执行
            allDone.Set();
            Console.WriteLine("连接已经建立");
            //将接收的连接传递近来
            Socket listener = (Socket)ar.AsyncState;
            //调用EndAccept方法表示连接已经建立，建立的连接就是该方法的返回的对象
            Socket handler = listener.EndAccept(ar);

            StateObject state = new StateObject();

            state.workSocket = handler;
            //这里又出现了类似的定义。可以看出，BeginReceive函数的参数和同步里面Receive函数的参数前面是相同的
            //只是后面多出了两个：定义在BeginReceive函数执行完毕以后所要执行的操作
            //这里定义的是ReadCallback函数
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
        }

        public static void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;

            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;
            //这里EndReceive函数不能理解为结束接收的意思，应该是至今为止接收到的意思
            //因为即使使用了该函数，如果数据没有接收完需要再次接收的时候，数据是在前面接收的基础上
            //接收剩余的部分
            int bytesRead =0;
            try
            {
                bytesRead = handler.EndReceive(ar);
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);   
            }
            //下面的if语句虽然没有用循环语句的表象，但是可以看到由于使用了递归的方法，因此整体可以看做是一个循环
            //该循环确保接收到所有的数据
            //当确定所有数据接受完毕后，会调用send方法
            if (bytesRead > 0)
            {
                // There  might be more data, so store the data received so far.
                state.sb.Append(Encoding.ASCII.GetString(
                    state.buffer, 0, bytesRead));
                content = state.sb.ToString();
                if (content.IndexOf("exit") > -1)
                {
                    Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                        content.Length, content);
                    Send(handler, content);
                }
                else
                {
                    // Not all data received. Get more.
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
                }
            }
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
        }

        private static void Send(Socket handler, String data)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // 这里和前面的解释类似
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket handler = (Socket)ar.AsyncState;
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }




        public void SocketAccept()
        {

            
            while (true)
            {
                
                try
                {
                    //accept
                    allDone.Reset();
                    sSocket.BeginAccept(new AsyncCallback(AcceptCallback), sSocket);
                    

                    allDone.WaitOne();
                }
                catch (SocketException ex)
                {
                    Console.WriteLine(ex.Message);
                    //serverSocket = sSocket.Accept();
                    //Console.WriteLine("连接已经建立");
                }

            }
        }
        public void CloseSocket()
        {
            serverSocket.Close();
            sSocket.Close();
        }
        public void Dispose()
        {
            Console.WriteLine("释放对象中资源...");
            serverSocket.Close();
            sSocket.Close();
        }
    }
}
