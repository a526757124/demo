using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class ClientPool
    {
        //private static List<TcpWork> clients = new List<TcpWork>();

        private static int freeCount;

        private static int workCount;

        private static int maxAllowed = 2;

        private static int minClients = 2;
        /// <summary>
        /// create new instance
        /// </summary>
        private ClientPool()
        {
        }

        private static ClientPool instance;
        private static readonly object syncInstanceObj = new object();
        public static ClientPool Singleton
        {
            get
            {
                if (instance == null)
                {
                    lock (syncInstanceObj)
                    {
                        if (instance == null)
                        {
                            instance = new ClientPool();
                        }
                    }
                }
                return instance;
            }
        }

        private static readonly object syncObj = new object();

        //public TcpWork GetClient()
        //{
        //    try
        //    {
        //        TcpWork work = new TcpWork();
        //        work.Connect("127.0.0.1", 8090);
        //        work.LingerState = new LingerOption(false, 3);
        //        work.IsWork = true;
        //        work.Expired = false;
        //        workCount++;
        //        lock (syncObj)
        //        {
        //            clients.Add(work);
        //        }
        //        Console.WriteLine(workCount);
        //        return work;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return null;
        //    }
        //}
    }
}
