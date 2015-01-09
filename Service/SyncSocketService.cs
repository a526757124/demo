using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    /// <summary>
    /// 同步
    /// </summary>
    public class SyncSocketService : IDisposable
    {
        Socket sSocket;
        Socket serverSocket;
        public SyncSocketService(string hostIP, int port, int listenNum = 0)
        {
            IPAddress ipAddress = IPAddress.Parse(hostIP);
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);
            sSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sSocket.Bind(ipEndPoint);
            sSocket.Listen(listenNum);
            
        }
        public void SocketAccept()
        {
            serverSocket = sSocket.Accept();
            Console.WriteLine("连接已经建立");
            while (true)
            {
                //receive message
                try
                {
                    string recStr = "";
                    byte[] recByte = new byte[4096];
                    int bytes = serverSocket.Receive(recByte, recByte.Length, 0);
                    recStr += Encoding.ASCII.GetString(recByte, 0, bytes);

                    //send message
                    Console.WriteLine("服务器端获得信息:{0}", recStr);
                    string sendStr = "send to client :OK";
                    byte[] sendByte = Encoding.ASCII.GetBytes(sendStr);
                    serverSocket.Send(sendByte, sendByte.Length, 0);
                    if (recStr.Equals("exit"))
                    {
                        Console.WriteLine("关闭此Socket连接");
                        break;
                    }
                }
                catch (SocketException ex)
                {
                    Console.WriteLine(ex.Message);
                    serverSocket = sSocket.Accept();
                    Console.WriteLine("连接已经建立");
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
