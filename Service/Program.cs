using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (SyncSocketService syncSocketService = new SyncSocketService("127.0.0.1", 12345))
            //{
            //    syncSocketService.SocketAccept();
            //}

            //using (AsyncSocketService asyncSocketService = new AsyncSocketService("127.0.0.1", 6000))
            //{
            //    asyncSocketService.SocketAccept();
            //}

            using (PosSocketService posSocketService = new PosSocketService("127.0.0.1", 12345))
            {
                posSocketService.SocketAccept();
            }
            Console.ReadLine();
        }
    }
}
