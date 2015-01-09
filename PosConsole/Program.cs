using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Configuration;
namespace PosConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            #region 测试注释


            //var barr= new byte[] { 0x00, 0x8D };
            //char c = '1';
            //string str = "1";

            ////List<byte> list = new List<byte>();
            ////foreach (var item in "李".ToCharArray())
            ////{
            ////    var b = System.Convert.ToString(item, 16);
            ////    list.Add(Tool.GetEncoding().GetBytes(b));
            ////}
            //var buff= Tool.GetEncoding().GetBytes("李俊");
            //var bitstr = BitConverter.ToString(buff).Split('-').Select(s => s).ToArray();
            //var get= Tool.GetEncoding().GetBytes("133");


            //Console.Read();
            byte[] list = new byte[]{
            0xd6, 0xd0, 0xb9, 
            0xfa,0xb9, 0xa4,0xc9,0xcc, 0xd2,0xf8,0xd0,
            0xd0,0xc4,0xb5,0xb5,0xa4,0xbf,0xa8,0x20
            ,0x20,0x20,0x20,0x20,0x20,0x20,0x20,0x20
            ,0x20,0x20,0x20,0x20,0x20,0x20,0x20,0x20
            ,0x20,0x20,0x20,0x20,0x20
        };
            var e = Encoding.GetEncoding("GBK").GetString(list);
            Console.WriteLine(e);
            string name = "中国工商银行牡丹卡";
            var en = Encoding.GetEncoding("GBK").GetBytes(name);
            //把ASCII码转化为对应的字符 
            ASCIIEncoding AE2 = new ASCIIEncoding();
            byte[] ByteArray2 = { 72, 101, 108, 108, 111, 32, 87, 111, 114, 108, 100 };
            char[] CharArray = AE2.GetChars(ByteArray2);
            for (int x = 0; x <= CharArray.Length - 1; x++)
            {
                Console.Write(CharArray[x]);
            }



            var u = Encoding.ASCII.GetBytes(name);

            var str = Tool.GetEncoding().GetChars(list);

            //RequestData data = new RequestData();
            //var v = DataEntityAttributeHelper.GetDataLength<ResponseData>(p => p.TPDU);
            #endregion

            using (AsyncSocketService asyncSocketService = new AsyncSocketService(int.Parse(ConfigurationManager.AppSettings["port"])))
            {

            }
            //TcpListenerSocketService tcpListen = new TcpListenerSocketService();
            #region MyRegion
            //TcpListener tcpListenerServer = new TcpListener(IPAddress.Any, 12345);
            //tcpListenerServer.Start();
            //Console.WriteLine("等待连接");
            //TcpClient tcpClient = tcpListenerServer.AcceptTcpClient();

            //while (true)
            //{
            //    Console.WriteLine("连接成功");
            //    try
            //    {
            //        byte[] data = new byte[1024];
            //        int bytes = tcpClient.Client.Receive(data, data.Length, 0);
            //        string recStr = Encoding.ASCII.GetString(data, 0, bytes);
            //        Console.WriteLine(recStr);

            //        string sendStr = "";
            //        byte[] sendBytes = Encoding.ASCII.GetBytes(sendStr);
            //        tcpClient.Client.Send(sendBytes);
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //        tcpClient.Close();
            //        Console.WriteLine("等待连接");
            //        tcpClient = tcpListenerServer.AcceptTcpClient();
            //    }
            //} 
            #endregion

        }
    }
}