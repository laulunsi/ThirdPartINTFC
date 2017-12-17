using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using ZIT.ThirdPartINTFC.Model;

namespace ZIT.ThirdPartINTFC.BLL.UDP.Base
{
    internal class Client
    {
        #region 变量

        private UdpClient client;

        public short localPort;

        public IPEndPoint remoteIPEP;

        #endregion 变量

        #region 构造函数

        public Client()
        {

        }

        #endregion 构造函数

        #region 方法

        public void Start()
        {
            client = new UdpClient(localPort);
            //非广播客户端模式
            if (remoteIPEP !=null)
            {
                if (PING(Convert.ToString(remoteIPEP.Address)))
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(ReceiveMsg));
                }
                else
                {
                    RaiseErrOccurEvent(new ErrMsg() { Message = string.Format("服务器主机地址Ping失败，地址：{0}", Convert.ToString(remoteIPEP.Address)) });
                }
            }
        }

        private void ReceiveMsg(object state)
        {
            while (true)
            {
                try
                {
                    if (client != null)
                    {
                        IPEndPoint recvipep = null;
                        byte[] buffer = client.Receive(ref recvipep);
                        if (recvipep != null && buffer.Length > 0)
                        {
                            string message = Encoding.ASCII.GetString(buffer);
                            RaiseReceiveEvent(message, recvipep);
                        }
                    }
                }
                catch (ObjectDisposedException)
                {
                    RaiseErrOccurEvent(new ErrMsg() { Message = "UdpClient 已关闭。" });
                }
                catch (Exception ex)
                {

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
            try
            {

                byte[] buff = Encoding.ASCII.GetBytes(message);
                if (client.Send(buff, buff.Length, remoteIPEP) == buff.Length)
                {
                    bln = true;
                    RaiseSendEvent(message, remoteIPEP);
                }
            }
            catch (ObjectDisposedException)
            {
                RaiseErrOccurEvent(new ErrMsg() { Message = "UdpClient 已关闭。" });
                bln = false;
            }
            catch (Exception ex)
            {

            }
            return bln;
        }

        /// <summary>
        /// Ping方法
        /// </summary>
        /// <param name="ipaddress"></param>
        /// <returns></returns>
        private bool PING(string ipaddress)
        {
            bool bln = false;
            Ping ping = new Ping();
            PingOptions pingOptions = new PingOptions(50, true);
            string data = "hello world";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            PingReply pingReply = ping.Send(ipaddress, 5, buffer);
            if (pingReply.Status == IPStatus.Success)
            {
                bln = true;
            }
            return bln;
        }
        #endregion 方法

        #region 委托

        /// <summary>
        /// 错误信息上报
        /// </summary>
        public event ErrOccurHandler ErrOccurEvent;

        public delegate void ErrOccurHandler(ErrMsg err);

        private void RaiseErrOccurEvent(ErrMsg err)
        {
            ErrOccurEvent?.Invoke(err);
        }

        /// <summary>
        /// 收到信息事件
        /// </summary>
        public event ReceiveHandler ReceiveEvent;

        public delegate void ReceiveHandler(string message, IPEndPoint ipep);

        private void RaiseReceiveEvent(string message, IPEndPoint ipep)
        {
            ReceiveEvent?.Invoke(message, ipep);
        }

        /// <summary>
        /// 发送信息事件
        /// </summary>
        public event SendHandler SendEvent;

        public delegate void SendHandler(string message, IPEndPoint ipep);

        private void RaiseSendEvent(string message, IPEndPoint ipep)
        {
            SendEvent?.Invoke(message, ipep);
        }

        #endregion 委托
    }
}