using System;
using System.Net;
using System.Threading.Tasks;
using LogUtility;
using ZIT.ThirdPartINTFC.BLL.UDP.Base;

namespace ZIT.ThirdPartINTFC.BLL.UDP
{
    public class GServer
    {
        #region 变量

        internal Client client = null;

        public static GServer instance = null;

        private short localPort;
        #endregion

        #region 构造方法

        public GServer()
        {
            client = new Client();
            client.ErrOccurEvent += Client_ErrOccurEvent;
            client.ReceiveEvent += Client_ReceiveEvent;
            client.localPort = localPort;
            client.Start();
        }
        #endregion

        #region 方法

        public static GServer GetInstance()
        {
            if (instance == null)
            {
                instance = new GServer();
            }
            return instance;
        }

        public void SendMsg(string message)
        {
            client.SendMsg(message);
        }

        private void HandleMessage(string message)
        {
            try
            {

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        #endregion

        #region 事件
        /// <summary>
        /// 收到消息事件
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ipep"></param>
        private void Client_ReceiveEvent(string message, System.Net.IPEndPoint ipep)
        {
            //写日志处理
            LogUtility.DataLog.WriteLog(LogLevel.Info, string.Format("地址：{0}收到消息:{1}", Convert.ToString(ipep), message), new RunningPlace("BSSClient", "Client_ReceiveEvent"), "FromBssServer");
            new Task(() => HandleMessage(message));

        }

        /// <summary>
        /// 通讯中断事件
        /// </summary>
        /// <param name="err"></param>
        private void Client_ErrOccurEvent(Model.ErrMsg err)
        {
            //写日志处理
            LogUtility.DataLog.WriteLog(LogLevel.Info, string.Format("BSSClient报错：{0}", err.Message), new RunningPlace("BSSClient", "Client_ErrOccurEvent"), "UdpErr");
            //显示连接中断
        }


        #endregion
    }
}