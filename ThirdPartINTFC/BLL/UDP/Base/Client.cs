using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using ZIT.ThirdPartINTFC.Utils;

namespace ZIT.ThirdPartINTFC.BLL.UDP.Base
{
    internal class Client
    {
        #region 变量

        /// <summary>
        /// Udp通讯客户端
        /// </summary>
        private UdpClient _client;

        /// <summary>
        /// 当前连接状态
        /// </summary>
        private bool _blnConnect;

        /// <summary>
        /// 上一次连接时间
        /// </summary>
        private DateTime _lastConTime;

        /// <summary>
        /// 本地端口号
        /// </summary>
        public short LocalPort;

        /// <summary>
        /// 目标服务器的IP与端口号，如果是接受广播消息则为null
        /// </summary>
        public IPEndPoint RemoteIpep;

        /// <summary>
        /// 握手消息内容
        /// </summary>
        public string HandShakeMsg;

        #endregion 变量

        #region 构造函数

        public Client()
        {
            _lastConTime = DateTime.MinValue;
            _blnConnect = false;
        }

        #endregion 构造函数

        #region 方法

        /// <summary>
        /// 启动
        /// </summary>
        public void Start()
        {
            _client = new UdpClient(LocalPort);
            //非广播客户端模式
            if (RemoteIpep != null)
            {
                if (Ping(Convert.ToString(RemoteIpep.Address)))
                {
                    //_blnConnect = true;
                    //RaiseConnected();
                    ThreadPool.QueueUserWorkItem(ReceiveMsg);
                    ThreadPool.QueueUserWorkItem(HandShake);
                }
                else
                {
                    _blnConnect = false;
                    RaiseDisConnected($"服务器主机地址Ping失败，地址：{Convert.ToString(RemoteIpep.Address)}");
                }
            }
            else
            {
                ThreadPool.QueueUserWorkItem(ReceiveMsg);
            }
            ThreadPool.QueueUserWorkItem(CheckHandShake);
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            _client.Close();
        }

        /// <summary>
        /// 发送握手消息
        /// </summary>
        /// <param name="state"></param>
        private void HandShake(object state)
        {
            while (Core.Flag)
            {
                SendMsg(HandShakeMsg, false);
                Thread.Sleep(1000 * SysParameters.SharkHandsInterval);
            }
        }

        /// <summary>
        /// 检测握手
        /// </summary>
        /// <param name="state"></param>
        private void CheckHandShake(object state)
        {
            while (Core.Flag)
            {
                if (_blnConnect && (DateTime.Now - _lastConTime).TotalSeconds > 30)
                {
                    _blnConnect = false;
                    RaiseDisConnected("握手超时，已断开");
                }
                Thread.Sleep(1000 * 5);
            }
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="state"></param>
        private void ReceiveMsg(object state)
        {
            IPEndPoint remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            while (Core.Flag)
            {
                try
                {
                    if (_client != null)
                    {
                        byte[] buffer = _client.Receive(ref remoteIpEndPoint);
                        _lastConTime = DateTime.Now;
                        if (!_blnConnect)
                        {
                            _blnConnect = true;
                            RaiseConnected();
                        }
                        if (remoteIpEndPoint != null && buffer.Length > 0)
                        {
                            string message = Encoding.Default.GetString(buffer);

                            if (message.IndexOf("[3000", StringComparison.Ordinal) < 0)
                            {
                                RaiseReceiveEvent(message, remoteIpEndPoint);
                            }
                        }
                        remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    }
                }
                catch (ObjectDisposedException e)
                {
                    RaiseDisConnected($"UdpClient 已关闭。{e.Message}");
                }
                catch (Exception ex)
                {
                    RaiseDisConnected(ex.Message);
                }
            }
        }

        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="message">发送的消息内容</param>
        /// /// <param name="blnLog">是否记录日志</param>
        /// <returns></returns>
        public bool SendMsg(string message, bool blnLog)
        {
            bool blnSend = false;
            try
            {
                byte[] buff = Encoding.Default.GetBytes(message);
                if (_client.Send(buff, buff.Length, RemoteIpep) == buff.Length) blnSend = true;
                if (blnLog) RaiseSendEvent(message, RemoteIpep);
            }
            catch (ObjectDisposedException e)
            {
                blnSend = false;
                RaiseDisConnected($"UdpClient 已关闭。{e.Message}");
            }
            catch (Exception ex)
            {
                blnSend = false;
                RaiseDisConnected(ex.Message);
            }
            return blnSend;
        }

        /// <summary>
        /// Ping方法
        /// </summary>
        /// <param name="ipaddress">目标计算机地址</param>
        /// <returns></returns>
        private bool Ping(string ipaddress)
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
        /// 连接事件
        /// </summary>
        public event ConnectedHandler Connected;

        public delegate void ConnectedHandler();

        private void RaiseConnected()
        {
            Connected?.Invoke();
        }

        /// <summary>
        /// 断开事件
        /// </summary>
        public event DisConnectedHandler DisConnected;

        public delegate void DisConnectedHandler(string info);

        private void RaiseDisConnected(string info)
        {
            DisConnected?.Invoke(info);
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