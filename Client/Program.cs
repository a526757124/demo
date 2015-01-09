using PosConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 9060;
            string host = "125.70.0.227";//服务器端ip地址


            #region 异步
            //using (var client= new AsyncSocketClient("127.0.0.1", 6000))
            //{
            //    try
            //    {
            //        while (true)
            //        {
            //            client.Send(Console.ReadLine());
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //    }
            //}
            #endregion



            #region 同步

            //IPAddress ip = IPAddress.Parse(host);
            //IPEndPoint ipe = new IPEndPoint(ip, port);

            //Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //clientSocket.Connect(ipe);
            ////while (true)
            ////{
            //    //send message
            //    string sendStr = Console.ReadLine();
            //    byte[] sendBytes = Encoding.UTF8.GetBytes(sendStr);
            //    clientSocket.Send(sendBytes);

            //    //receive message
            //    string recStr = "";
            //    byte[] recBytes = new byte[4096];
            //    int bytes = clientSocket.Receive(recBytes, recBytes.Length, 0);
            //    recStr += Encoding.UTF8.GetString(recBytes, 0, bytes);
            //    Console.WriteLine(recStr);
            ////}
            //clientSocket.Close();
            #endregion

            #region 测试
            int cout = 0;
            for (int i = 0; i < 10000; i++)
            {
                try
                {
                    ThreadStart threadStart = new ThreadStart(ThreadSocket);

                    Thread thread = new Thread(threadStart);

                    thread.Start();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    cout++;
                }
            }

            #endregion
            Console.WriteLine("失败：" + cout);
            Console.Read();
        }
        static void ThreadSocket()
        {
            int port = 9060;
            string host = "127.0.0.1";//服务器端ip地址
            IPAddress ip = IPAddress.Parse(host);
            IPEndPoint ipe = new IPEndPoint(ip, port);

            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            clientSocket.Connect(ipe);

            //send message
            //string sendStr = "004D6000080000303639313632323538383032383631333237313820202030303030303030303030303134343032343130353931353530303333383338323137303230313431303130313035303537";
            byte[] sendBytes = new byte[]{
                0x00,0x85,
                0x60,0x00,0x08,0x00,0x00,
                0x2D,0x4E,0xBC,0x41,0xE6,0x0C,0x46,0x64,
                0x60,0x37,0x5C,0x54,0x4F,0x23,0xAD,0xB9,
                0x79,0xBF,0x82,0x8C,0xFD,0x9F,0xD7,0x77,
                0x3C,0x2F,0xF4,0xD7,0x48,0xC6,0xC4,0x5B,
                0x67,0xBE,0x16,0x51,0x43,0xE7,0x9B,0x1C,
                0xF1,0x3F,0xAD,0xC0,0xED,0xFC,0x1F,0xE1,
                0x53,0x0B,0xE2,0x4D,0xE3,0xB8,0xBE,0xD1,
                0xA6,0xFB,0xE2,0x70,0x54,0x12,0x41,0x9C,
                0xA0,0x50,0x39,0x4D,0x05,0x7D,0xE2,0x40,
                0xC4,0xBE,0xF0,0x0A,0xA0,0x98,0xE1,0x13,
                0xEA,0x35,0xC3,0x3E,0x2D,0xFA,0x7D,0x4F,
                0x56,0x9D,0xFE,0x02,0xD0,0x7E,0x56,0x2D,
                0x29,0x06,0xA8,0xA4,0xF8,0x26,0xEB,0x28,
                0x9D,0x55,0x36,0x5C,0xCF,0x71,0xE6,0x85,
                0x5A,0x8F,0x94,0xDC,0x00,0xBE,0x97,0x50,
                0x5A,0x8F,0x94,0xDC,0x00,0xBE,0x97,0x50
            };
            clientSocket.Send(sendBytes);

            //receive message
            string recStr = "";
            byte[] recBytes = new byte[512];
            int bytes = clientSocket.Receive(recBytes, recBytes.Length, 0);
            byte[] recB = new byte[192];
            for (int i = 0; i < 192; i++)
            {
                recB[i] = recBytes[i + 7];
            }
            //var abc = Tool.RemoveLeftByte(recBytes, 7);
            byte[] outDes = new byte[192];
            byte b = 0;
            Tool.HKDes(ref recB[0], ref outDes[0], recB.Length, b);
            recStr += Tool.GetGBKEncoding().GetString(outDes, 0, 192);
            Console.WriteLine(recStr);
            //}
            clientSocket.Close();

        }

    }
}
