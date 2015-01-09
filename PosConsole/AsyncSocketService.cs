using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PosConsole
{
    // 将用到的变量包装一下
    // 保存当前Socket状态的类
    public class StateObject : IDisposable
    {
        public Socket socket = null;
        public const int bufferSize = 1024;
        public byte[] buffer = new byte[bufferSize];
        /// <summary>
        /// 接收到的所有byte数据
        /// </summary>
        public List<byte> bufferAll = new List<byte>();
        /// <summary>
        /// 长度
        /// </summary>
        public byte[] byteLen = new byte[2];
        /// <summary>
        /// BCD码
        /// </summary>
        public byte[] byteBCD = new byte[5];
        /// <summary>
        /// 排除长度与BCD码内容
        /// </summary>
        public List<byte> byteBody = new List<byte>();
        /// <summary>
        /// 接收到的字节
        /// </summary>
        public int bytesRead = 0;
        public StringBuilder sb = new StringBuilder();

        public void Dispose()
        {
            if (socket != null)
                socket.Close();
        }
    }
    /// <summary>
    /// 异步
    /// </summary>
    public class AsyncSocketService : IDisposable
    {
        private static ILog _iLog = LogManager.GetLogger("Logger");
        public AsyncSocketService()
        {

        }
        public AsyncSocketService(int port, int backlog = 0)
        {
            log4net.Config.XmlConfigurator.Configure();
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, port);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //设置KeeyAlive，如果客户端不主动发消息时，Tcp本身会发一个心跳包，来通知服务器，这是一个保持通讯的链接。
            //避免等到下一次通讯时，才知道链接已经断开。
            //socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
            //socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.DontLinger, true);
            socket.Bind(ipEndPoint);
            socket.Listen(1000);
            Console.WriteLine("服务已启动,等待客户端连接...");
            SocketAccept(socket);

        }

        // 控制线程的的对象
        public static ManualResetEvent allDone = new ManualResetEvent(false);
        public static ManualResetEventSlim done = new ManualResetEventSlim(false);
        /// <summary>
        /// AcceptCallback是回调函数
        /// </summary>
        /// <param name="ar"></param>
        public static void AcceptCallback(IAsyncResult ar)
        {
            // 接收连接后，按照前面的定义会执行该函数，首先就是让主线程继续往下执行
            allDone.Set();
            count++;
            Console.WriteLine("连接已经建立");

            try
            {
                //将接收的连接传递进来
                Socket listener = (Socket)ar.AsyncState;

                //调用EndAccept方法表示连接已经建立，建立的连接就是该方法的返回的对象
                Socket handler = listener.EndAccept(ar);

                //保存当前会话的Socket信息
                StateObject state = new StateObject();

                state.socket = handler;

                //这里又出现了类似的定义。可以看出，BeginReceive函数的参数和同步里面Receive函数的参数前面是相同的
                //只是后面多出了两个：定义在BeginReceive函数执行完毕以后所要执行的操作
                //这里定义的是ReadCallback函数
                handler.BeginReceive(state.buffer, 0, StateObject.bufferSize, 0,
                    new AsyncCallback(ReadCallback), state);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _iLog.Error(ex.Message);
            }

        }
        /// <summary>
        /// 接收数据 回调函数
        /// </summary>
        /// <param name="ar"></param>
        public static void ReadCallback(IAsyncResult ar)
        {
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.socket;
            //这里EndReceive函数不能理解为结束接收的意思，应该是至今为止接收到的意思
            //因为即使使用了该函数，如果数据没有接收完需要再次接收的时候，数据是在前面接收的基础上
            //接收剩余的部分
            int bytesRead = 0;
            try
            {
                bytesRead = handler.EndReceive(ar);
                state.bytesRead += bytesRead;
                //下面的if语句虽然没有用循环语句的表象，但是可以看到由于使用了递归的方法，因此整体可以看做是一个循环
                //该循环确保接收到所有的数据
                //当bytesRead<=0时，确定所有数据接受完毕，调用send方法
                if (bytesRead > 0)
                {
                    for (int i = 0; i < bytesRead; i++)
                    {
                        state.bufferAll.Add(state.buffer[i]);
                    }



                    Console.WriteLine("把byte转换为16进制：{0}", BitConverter.ToString(state.bufferAll.ToArray()));

                    _iLog.InfoFormat("把byte转换为16进制：{0}", BitConverter.ToString(state.bufferAll.ToArray()));

                    //Console.WriteLine("把byte转换为10进制：{0}",BitConverter.ToInt32(state.buffer,0));
                    //_iLog.InfoFormat("把byte转换为10进制：{0}", BitConverter.ToInt32(state.buffer, 0));


                    if (handler.Poll(0, SelectMode.SelectRead))
                    {
                        //数据没读取完
                        handler.BeginReceive(state.buffer, 0, StateObject.bufferSize, 0,
                            new AsyncCallback(ReadCallback), state);
                    }
                    else
                    {

                        try
                        {
                            state = Tool.DealData(state);
                            state.sb.Append(Tool.GetEncoding().GetString(state.byteBody.ToArray(), 0,
                                state.byteBody.Count - DataEntityAttributeHelper.GetDataLength<RequestData>(p => p.CARDNAME)));
                            state.sb.Append(Tool.GetGBKEncoding().GetString(state.byteBody.ToArray(),
                                state.byteBody.Count - DataEntityAttributeHelper.GetDataLength<RequestData>(p => p.CARDNAME), DataEntityAttributeHelper.GetDataLength<RequestData>(p => p.CARDNAME)));
                            Console.WriteLine("--------------------接收到的数据【{0}】--------------------", Tool.GetDate());
                            Console.WriteLine("Read Data:{0}", state.sb.ToString());
                            Console.WriteLine("Read {0} bytes .", state.bytesRead);
                            Console.WriteLine("-------------------------------------------------------------------------------");
                            _iLog.InfoFormat("-------------------------接收到的数据-------------------------");
                            _iLog.InfoFormat("Read Data:{0}", state.sb.ToString());
                            _iLog.InfoFormat("Read {0} bytes .", state.bytesRead);
                            _iLog.InfoFormat("--------------------------------------------------------------");

                            var data = DataEntityHelper.ToRequestData(state.byteBCD, state.sb.ToString());

                            //TODO 调用钱袋子api
                            //var tradeData = Tool.Notify(data);
                            var tradeData = new Trade().GetTradeData();

                            Console.WriteLine("-----------------返回交易状态的数据【{0}】-----------------", Tool.GetDate());
                            Console.WriteLine(tradeData.ToString());
                            Console.WriteLine("-------------------------------------------------------------------------------");
                            _iLog.InfoFormat("----------------------返回交易状态的数据----------------------");
                            _iLog.InfoFormat(tradeData.ToString());
                            _iLog.InfoFormat("--------------------------------------------------------------");

                            var content = DataEntityHelper.ToResponseData(data, tradeData);
                            Console.WriteLine(content.Body.ToString());
                            Send(handler, content);
                        }
                        catch (Exception ex)
                        {
                            //Send(handler, ex.Message);
                            Console.WriteLine(ex.Message);
                            _iLog.Error(ex);
                        }

                    }
                }
                ////继续接收客户端发送的数据
                ////清空数据
                state.sb.Clear();
                state.bytesRead = 0;
                handler.BeginReceive(state.buffer, 0, StateObject.bufferSize, 0,
                            new AsyncCallback(ReadCallback), state);
            }
            catch (SocketException ex)
            {
                //客户端关闭时，断开连接
                if (ex.SocketErrorCode == SocketError.Shutdown)
                {
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
                Console.WriteLine(ex.Message);
                _iLog.Error(ex.Message);
            }
        }
        /// <summary>
        /// 发送数据 
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="data"></param>
        private static void Send(Socket handler, object data)
        {
            try
            {
                var sendData = data as DataEntity<ResponseData>;

                List<byte> byteData = new List<byte>();
                if (sendData != null)
                {
                    //byteData.AddRange(sendData.Header.ToByte());
                    //byteData.AddRange(sendData.Body.ToByte());
                    byteData.AddRange(sendData.Body.databuf.ToByte());
                    Console.WriteLine("--------------------应答响应数据【{0}】--------------------", Tool.GetDate());
                    Console.WriteLine("Send Data:{0}", sendData.ToString());
                    Console.WriteLine("-------------------------------------------------------------------------------");

                    _iLog.InfoFormat("-------------------------应答响应数据-------------------------");
                    _iLog.InfoFormat("Send Data:{0}", sendData.ToString());
                    _iLog.InfoFormat("--------------------------------------------------------------");

                    Console.WriteLine("发送数据：把byte转换为16进制：{0}", BitConverter.ToString(byteData.ToArray()));

                    _iLog.InfoFormat("发送数据：把byte转换为16进制：{0}", BitConverter.ToString(byteData.ToArray()));
                }
                else
                {
                    byteData.AddRange(Tool.GetEncoding().GetBytes(data.ToString()));
                }



                //开始发送
                handler.BeginSend(byteData.ToArray(), 0, byteData.Count, 0,
                    new AsyncCallback(SendCallback), handler);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _iLog.Error(ex);
            }


        }
        /// <summary>
        /// 发送数据 回调函数
        /// </summary>
        /// <param name="ar"></param>
        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket handler = (Socket)ar.AsyncState;
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);

                //handler.Shutdown(SocketShutdown.Both);
                //handler.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                _iLog.Error(ex.Message);
            }
        }
        static int count = 0;
        /// <summary>
        /// 执行方法
        /// </summary>
        private void SocketAccept(Socket socket)
        {
            while (true)
            {
                try
                {
                    //线程阻塞
                    allDone.Reset();
                    socket.BeginAccept(new AsyncCallback(AcceptCallback), socket);
                    allDone.WaitOne();
                }
                catch (SocketException ex)
                {
                    allDone.Set();
                    Console.WriteLine(ex.Message);
                    _iLog.Error(ex.Message);
                }
            }
        }

        public void Dispose()
        {
            Console.WriteLine("释放对象中资源...");
            allDone.Close();
        }
    }

    public class Trade
    {
        static int count = 0;
        public TradeResultData GetTradeData()
        {
            string no = DateTime.Now.ToString("yyyyMMddhhmmss") + (++count).ToString();
            var data = new TradeResultData()
            {
                Code = "0",
                OrderNo = no,
                ErrorMsg = ""

            };
            return data;
        }
    }


    public class ConvertEx
    {
        //16进制字符数组  
        private static char[] hex = new char[] {   
        '0', '1', '2', '3', '4', '5', '6', '7',  
        '8', '9', 'a', 'b', 'c', 'd', 'e', 'f'  
};
        public static String byte2HexString(byte[] b)
        {
            if (b == null)
                return "null";

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < b.Length; i++)
            {
                sb.Append(hex[b[i] >> 4])
                    .Append(hex[b[i] & 0xF]);
            }
            return sb.ToString();
        }
        public static byte[] AscToBCD(byte[] AscStr)
        {
            List<byte> BcdStr = new List<byte>();
            byte[] str = new byte[2];
            byte t_buff;
            for (int i = 0; i < AscStr.Length; i += 2)
            {
                t_buff = AscStr[i];
                if ((t_buff >= 'a') && (t_buff <= 'f'))
                    str[0] = (byte)(t_buff - 'a' + 0x0A);
                else if ((t_buff >= 'A') && (t_buff <= 'F'))
                    str[0] = (byte)(t_buff - 'A' + 0x0A);
                else if (t_buff >= '0')
                    str[0] = (byte)(t_buff - '0');
                else
                    str[0] = 0;

                if ((i + 2) > AscStr.Length) t_buff = (byte)'0';
                else t_buff = AscStr[i + 1];
                if ((t_buff >= 'a') && (t_buff <= 'f'))
                    str[1] = (byte)(t_buff - 'a' + 0x0A);
                else if ((t_buff >= 'A') && (t_buff <= 'F'))
                    str[1] = (byte)(t_buff - 'A' + 0x0A);
                else if (t_buff >= '0')
                    str[1] = (byte)(t_buff - '0');
                else
                    str[1] = 0;

                BcdStr[i / 2] = (byte)((str[0] << 4) | (str[1] & 0x0F));
            }
            return BcdStr.ToArray();
        }

        /** 
           其他类型的转化同int 
          */
    }

}
