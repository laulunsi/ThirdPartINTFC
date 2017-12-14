using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZIT.ThirdPartINTFC.BLL.BSS
{
    internal class Client
    {
        #region 变量
        private UdpClient client;

        private short localPort;

        private IPEndPoint remoteIPEP;

        private ConcurrentQueue<string> RecvQueue;

        private ConcurrentQueue<string> SendQueue;

        public static UdpClient instance = null;
        #endregion

        #region 构造函数

        public Client(short port, IPEndPoint remote)
        {
            client = new UdpClient(port);
            RecvQueue = new ConcurrentQueue<string>();
            SendQueue = new ConcurrentQueue<string>();
            if (IPcanFind(Convert.ToString(remote.Address)))
            {
                remoteIPEP = remote;
            }
            else
            {
                RaiseErrOccurEvent(new ErrMsg() { message = string.Format("服务器主机地址Ping失败，地址：{0}", Convert.ToString(remote.Address)) });
            }
        }

        #endregion
        #region 方法


        private bool IPcanFind(string ipaddress)
        {
            bool bln = false;
            Ping ping = new Ping();
            PingOptions pingOptions = new PingOptions(50,true);
            string data = "hello world";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            PingReply pingReply = ping.Send(ipaddress, 5, buffer);
            if (pingReply.Status == IPStatus.Success)
            {
                bln = true;
            }
            return bln;
        }

        public void StartListen()
        {
            ThreadPool.QueueUserWorkItem(Handshake);
            ThreadPool.QueueUserWorkItem(ReceiveMsg);
        }



        private void Handshake(object state)
        {
            throw new NotImplementedException();
        }

        private void ReceiveMsg(object state)
        {
            while (true)
            {
                if (client != null)
                {
                    IPEndPoint recvipep = null;
                    byte[] buffer =  client.Receive(ref recvipep);
                    if (recvipep != null)
                    {
                        
                    }
                    if (buffer.Length>0)
                    {
                        string Msg = Encoding.ASCII.GetString(buffer);
                    }
                }
            }
            
        }

        public bool SendMsg()
        {
            bool bln = false;
            while (SendQueue.Count > 0)
            {
                if (SendQueue.TryPeek(out string message))
                {
                    byte[] buff = Encoding.ASCII.GetBytes(message);
                    if (client.Send(buff, buff.Length, remoteIPEP) == buff.Length)
                    {
                        SendQueue.TryDequeue(out message);
                    }
                }
            }
            Thread.Sleep(1000);
        }

        #endregion

        #region 委托
        /// <summary>
        /// 错误信息上报
        /// </summary>
        public event ErrOccurHandler ErrOccurEvent;
        public delegate void ErrOccurHandler(ErrMsg err);
        public void RaiseErrOccurEvent(ErrMsg err)
        {
            ErrOccurEvent?.Invoke(err);
        }



        #endregion
    }

    public class ErrMsg
    {
        public string message;
    }
}
