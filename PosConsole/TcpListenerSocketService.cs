using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PosConsole
{
    public class TcpListenerSocketService
    {
        private int maxLink = 100000;
        private int currentLinked;
        private ManualResetEvent tcpClientConnected = new ManualResetEvent(false);
        public TcpListenerSocketService()
        {
            TcpListener server = new TcpListener(new System.Net.IPEndPoint(IPAddress.Any, 12345));
            server.Start(0);
            tcpClientConnected.Reset();
            IAsyncResult result = server.BeginAcceptTcpClient(new AsyncCallback(Acceptor), server);
            tcpClientConnected.WaitOne();
        }
        private void Acceptor(IAsyncResult o)
        {
            TcpListener server = o.AsyncState as TcpListener;
            Debug.Assert(server != null);
            TcpClient client = null;
            try
            {
                client = server.EndAcceptTcpClient(o);
                byte[] bytes=new byte[1024];
                //var stream = client.Client.BeginReceive(, 0,new AsyncCallback(ReadCallback), client);
                
                System.Threading.Interlocked.Increment(ref currentLinked);

            }
            catch
            {

            }
            IAsyncResult result = server.BeginAcceptTcpClient(new AsyncCallback(Acceptor), server);
            if (client == null)
            {
                return;
            }
            else
            {
                Thread.CurrentThread.Join();
            }
            Close(client);
        }
        public  void ReadCallback(IAsyncResult ar)
        {
        }
        private void Close(TcpClient client)
        {
            if (client.Connected)
            {
                client.Client.Shutdown(SocketShutdown.Both);
            }
            client.Client.Close();
            client.Close();

            System.Threading.Interlocked.Decrement(ref currentLinked);
        }
    }
}
