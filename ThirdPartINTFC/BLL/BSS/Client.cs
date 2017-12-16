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
using ZIT.ThirdPartINTFC.Model;

namespace ZIT.ThirdPartINTFC.BLL.BSS
{
    internal class Client
    {
        #region 变量
        private UdpClient client;

        private short localPort;

        private IPEndPoint remoteIPEP;

        public ConcurrentQueue<string> RecvMsg;
        #endregion

        #region 构造函数

        public Client(short port, IPEndPoint remote)
        {
            client = new UdpClient(port);
            if (PING(Convert.ToString(remote.Address)))
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


        private bool PING(string ipaddress)
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
            ThreadPool.QueueUserWorkItem(ReceiveMsg);
        }


        private void ReceiveMsg(object state)
        {
            while (true)
            {
                if (client != null)
                {
                    IPEndPoint recvipep = null;
                    byte[] buffer =  client.Receive(ref recvipep);
                    if (recvipep != null && buffer.Length > 0)
                    {
                        string message = Encoding.ASCII.GetString(buffer);
                        RaiseReceiveEvent(message, recvipep);
                    }
                }
            }
            
        }

        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool SendMsg(string message)
        {
            bool bln = false;
            byte[] buff = Encoding.ASCII.GetBytes(message);
            if (client.Send(buff, buff.Length, remoteIPEP) == buff.Length)
            {
                bln = true;
                RaiseSendEvent(message,remoteIPEP);
            }
            return bln;
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

        /// <summary>
        /// 收到信息事件
        /// </summary>
        public event ReceiveHandler ReceiveEvent;
        public delegate void ReceiveHandler(string message, IPEndPoint ipep);
        public void RaiseReceiveEvent(string message, IPEndPoint ipep)
        {
            ReceiveEvent?.Invoke(message,ipep);
        }

        /// <summary>
        /// 发送信息事件
        /// </summary>
        public event SendHandler SendEvent;
        public delegate void SendHandler(string message, IPEndPoint ipep);
        public void RaiseSendEvent(string message, IPEndPoint ipep)
        {
            SendEvent?.Invoke(message, ipep);
        }

        #endregion
    }


}
