using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class AsyncSocketClient:IDisposable
    {
        Socket sSocket;
        Socket clientSocket;
        public AsyncSocketClient(string hostIP, int port)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress ipAddress = IPAddress.Parse(hostIP);
            IPEndPoint endPoint = new IPEndPoint(ipAddress, port);
            try
            {
                clientSocket.Connect(endPoint);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
        
        public void Send(String sendMeg)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(sendMeg);
            clientSocket.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), clientSocket);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket handler = (Socket)ar.AsyncState;
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to service.", bytesSent);

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
                Console.WriteLine("Sent OK!");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


        private void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;

            //StateObject state = (StateObject)ar.AsyncState;
            //Socket handler = state.workSocket;
            ////这里EndReceive函数不能理解为结束接收的意思，应该是至今为止接收到的意思
            ////因为即使使用了该函数，如果数据没有接收完需要再次接收的时候，数据是在前面接收的基础上
            ////接收剩余的部分
            //int bytesRead = 0;
            //try
            //{
            //    bytesRead = handler.EndReceive(ar);
            //}
            //catch (SocketException ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            ////下面的if语句虽然没有用循环语句的表象，但是可以看到由于使用了递归的方法，因此整体可以看做是一个循环
            ////该循环确保接收到所有的数据
            ////当确定所有数据接受完毕后，会调用send方法
            //if (bytesRead > 0)
            //{
            //    // There  might be more data, so store the data received so far.
            //    state.sb.Append(Encoding.ASCII.GetString(
            //        state.buffer, 0, bytesRead));
            //    content = state.sb.ToString();
            //    if (content.IndexOf("exit") > -1)
            //    {
            //        Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
            //            content.Length, content);
            //        Send(handler, content);
            //    }
            //    else
            //    {
            //        // Not all data received. Get more.
            //        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
            //        new AsyncCallback(ReadCallback), state);
            //    }
            //}
        }





        public void Dispose()
        {
            Console.WriteLine("释放对象中资源...");
            clientSocket.Close();
            sSocket.Close();
        }
    }
}
