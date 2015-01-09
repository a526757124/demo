using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class PosSocketService:IDisposable
    {
        Socket sSocket;
        Socket serverSocket;
        public PosSocketService(string hostIP, int port, int listenNum = 0)
        {
            IPAddress ipAddress = IPAddress.Parse(hostIP);
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);
            sSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sSocket.Bind(ipEndPoint);
            sSocket.Listen(listenNum);
            Console.WriteLine("服务器已打开，等待连接...");
        }
        public void SocketAccept()
        {
            
            serverSocket = sSocket.Accept();
            Console.WriteLine("连接已经建立");
            while (true)
            {
                
                try
                {
                    Console.Write("服务器发送信息：");
                    

                    //send message
                    
                    string sendStr = Console.ReadLine();
                    byte[] sendByte = Encoding.ASCII.GetBytes(sendStr);
                    serverSocket.Send(sendByte, sendByte.Length, 0);
                    //if (recStr.Equals("exit"))
                    //{
                    //    Console.WriteLine("关闭此Socket连接");
                    //    break;
                    //}

                    //receive message
                    string recStr = "";
                    byte[] recByte = new byte[4096];
                    int bytes = serverSocket.Receive(recByte, recByte.Length, 0);
                    recStr += Encoding.ASCII.GetString(recByte, 0, bytes);
                    Console.WriteLine("服务器端获得信息:{0}", recStr);
                    
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
