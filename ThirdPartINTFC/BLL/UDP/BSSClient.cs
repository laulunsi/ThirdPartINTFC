﻿using LogUtility;
using System;
using System.Net;
using System.Threading.Tasks;
using ZIT.ThirdPartINTFC.BLL.UDP.Base;

namespace ZIT.ThirdPartINTFC.BLL.UDP
{
    public class BssClient
    {
        #region 变量

        private BSMessageHandler _handler;

        internal Client Client = null;

        public static BssClient Instance = null;

        public short LocalPort;

        public IPEndPoint RemoteIpep;

        public string HandShakeMsg;

        #endregion 变量

        #region 构造方法

        public BssClient()
        {
            _handler = new BSMessageHandler();
            Client = new Client();
        }

        #endregion 构造方法

        #region 方法

        public void Start()
        {
            Client.HandShakeMsg = HandShakeMsg;
            Client.ReceiveEvent += Client_ReceiveEvent;
            Client.SendEvent += Client_SendEvent;
            Client.LocalPort = LocalPort;
            Client.RemoteIpep = RemoteIpep;
            Client.Start();
            LogUtility.DataLog.WriteLog(LogLevel.Info, "BSSClient已启动", new RunningPlace("BssClient", "Start"), "OP");
        }

        public static BssClient GetInstance()
        {
            if (Instance == null)
            {
                Instance = new BssClient();
            }
            return Instance;
        }

        public void Close()
        {
            Client.Stop();
        }

        public bool SendMsg(string message)
        {
            return Client.SendMsg(message, true);
        }

        #endregion 方法

        #region 事件

        /// <summary>
        /// 发送消息事件
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ipep"></param>
        private void Client_SendEvent(string message, System.Net.IPEndPoint ipep)
        {
            //写日志处理
            LogUtility.DataLog.WriteLog(LogLevel.Info, string.Format("向地址：{0}发送消息:{1}", Convert.ToString(ipep), message), new RunningPlace("BSSClient", "Client_SendEvent"), "ToBssServer");
        }

        /// <summary>
        /// 收到消息事件
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ipep"></param>
        private void Client_ReceiveEvent(string message, System.Net.IPEndPoint ipep)
        {
            //写日志处理
            LogUtility.DataLog.WriteLog(LogLevel.Info, string.Format("地址：{0}收到消息:{1}", Convert.ToString(ipep), message), new RunningPlace("BSSClient", "Client_ReceiveEvent"), "FromBssServer");
            Task.Factory.StartNew(() => _handler.HandleMessage(message));
        }

        #endregion 事件
    }
}