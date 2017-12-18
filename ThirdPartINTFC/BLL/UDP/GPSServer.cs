using LogUtility;
using System;
using System.Threading.Tasks;
using ZIT.ThirdPartINTFC.BLL.UDP.Base;

namespace ZIT.ThirdPartINTFC.BLL.UDP
{
    public class GServer
    {
        #region 变量

        private MessageHandler _handler;

        internal Client Client = null;

        public short LocalPort;

        #endregion 变量

        #region 构造方法

        public GServer()
        {
            _handler = new MessageHandler();
            Client = new Client();
        }

        #endregion 构造方法

        #region 方法

        public void Start()
        {
            Client.ReceiveEvent += Client_ReceiveEvent;
            Client.LocalPort = LocalPort;
            Client.Start();
            LogUtility.DataLog.WriteLog(LogLevel.Info, "GPSClient已启动", new RunningPlace("GServer", "Start"), "OP");
        }

        public void Close()
        {
            Client.Stop();
        }

        public void SendMsg(string message)
        {
            Client.SendMsg(message, true);
        }

        #endregion 方法

        #region 事件

        /// <summary>
        /// 收到消息事件
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ipep"></param>
        private void Client_ReceiveEvent(string message, System.Net.IPEndPoint ipep)
        {
            //写日志处理
            LogUtility.DataLog.WriteLog(LogLevel.Info, string.Format("地址：{0}收到消息:{1}", Convert.ToString(ipep), message), new RunningPlace("GServer", "Client_ReceiveEvent"), "FromBssServer");
            var task = new Task(() => _handler.HandleMessage(message));
        }

        #endregion 事件
    }
}