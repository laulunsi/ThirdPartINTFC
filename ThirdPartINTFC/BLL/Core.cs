using LogUtility;
using System;
using System.Collections.Generic;
using System.Net;
using ZIT.ThirdPartINTFC.BLL.DA;
using ZIT.ThirdPartINTFC.BLL.UDP;
using ZIT.ThirdPartINTFC.Model;
using ZIT.ThirdPartINTFC.Utils;

namespace ZIT.ThirdPartINTFC.BLL
{
    public class Core
    {
        #region 变量

        /// <summary>
        /// 业务服务器连接
        /// </summary>
        public BssClient Bs;

        /// <summary>
        /// GPS服务器连接
        /// </summary>
        public GServer Gs;

        /// <summary>
        /// 数据库连接
        /// </summary>
        private DataAnalysis DA;

        /// <summary>
        /// 业务信息集合
        /// </summary>
        public List<Business> BussMap;

        /// <summary>
        /// 业务信息集合
        /// </summary>
        public List<string> VehMap;

        /// <summary>
        /// Core实例
        /// </summary>
        public static Core Instance = null;

        #endregion 变量

        #region 构造函数

        public Core()
        {
            //获取当前未完成的任务
            //BussMap = (List<Business>)InfoBll.Get_BUSSINFO();
            VehMap = new List<string>();
            //初始化业务服务器连接
            Bs = new BssClient
            {
                LocalPort = SysParameters.BssLocalPort,
                RemoteIpep = new IPEndPoint(IPAddress.Parse(SysParameters.BssServerIp), SysParameters.BssServerPort)
            };
            Bs.HandShakeMsg =
                $"[[3000DWBH:{SysParameters.LocalUnitCode}*#DWMC:*#ZJM:{GetZJM()}*#TLX:TFC*#TH:1*#ZBY:*#ZT:1*#LSH:*#ZBBC:*#]";
            Bs.Client.Connected += BSClient_Connected;
            Bs.Client.DisConnected += BSClient_DisConnected;
            //初始化GPS业务服务器连接
            Gs = new GServer { LocalPort = SysParameters.GpsLocalPort };
            Gs.Client.Connected += GSClient_Connected;
            Gs.Client.DisConnected += GSClient_DisConnected;
            //初始化数据库连接
            DA = new DataAnalysis();
            DA.Connected += DA_Connected;
            DA.DisConnected += DA_DisConnected;
        }

        #endregion 构造函数

        #region 方法

        /// <summary>
        /// 获取唯一实例
        /// </summary>
        /// <returns></returns>
        public static Core GetInstance()
        {
            if (Instance == null)
            {
                Instance = new Core();
            }
            return Instance;
        }

        /// <summary>
        /// 开始
        /// </summary>
        public void Start()
        {
            try
            {
                Bs.Start();
                Gs.Start();
                DA.Start();
            }
            catch (Exception e)
            {
                LogUtility.DataLog.WriteLog(LogLevel.Info, e.Message, new RunningPlace("Core", "Start"), "UdpErr");
            }
        }

        public void Stop()
        {
            try
            {
                Bs.Close();
                Gs.Close();
            }
            catch (Exception e)
            {
                LogUtility.DataLog.WriteLog(LogLevel.Info, e.Message, new RunningPlace("Core", "Start"), "UdpErr");
            }
        }

        /// <summary>
        /// 获取主机名
        /// </summary>
        /// <returns></returns>
        private string GetZJM()
        {
            string hostName = "";
            try
            {
                hostName = Dns.GetHostName();
            }
            catch (Exception ex)
            {
                LogUtility.DataLog.WriteLog(LogUtility.LogLevel.Info, ex.Message, new LogUtility.RunningPlace("BSSServer", "GetZJM"), "业务异常");
            }
            return hostName;
        }

        #endregion 方法

        #region 事件

        private void BSClient_Connected()
        {
            //显示连接成功
            RaiseStatusChangedEvent(new StatusChanged() { Status = ConStatus.Connected, Module = FunModule.Bs });
        }

        private void BSClient_DisConnected(string info)
        {
            //写日志处理
            LogUtility.DataLog.WriteLog(LogLevel.Info, string.Format("BSSClient报错：{0}", info), new RunningPlace("Core", "BSClient_DisConnected"), "UdpErr");
            //显示连接中断
            RaiseStatusChangedEvent(new StatusChanged() { Status = ConStatus.DisConnected, Module = FunModule.Bs });
        }

        private void GSClient_Connected()
        {
            //显示连接成功
            RaiseStatusChangedEvent(new StatusChanged() { Status = ConStatus.Connected, Module = FunModule.Gs });
        }

        private void GSClient_DisConnected(string info)
        {
            //写日志处理
            LogUtility.DataLog.WriteLog(LogLevel.Info, string.Format("GPSClient报错：{0}", info), new RunningPlace("Core", "GSClient_DisConnected"), "UdpErr");
            //显示连接中断
            RaiseStatusChangedEvent(new StatusChanged() { Status = ConStatus.DisConnected, Module = FunModule.Gs });
        }

        private void DA_Connected()
        {
            //显示连接成功
            RaiseStatusChangedEvent(new StatusChanged() { Status = ConStatus.Connected, Module = FunModule.Db });
        }

        private void DA_DisConnected(string info)
        {
            //写日志处理
            LogUtility.DataLog.WriteLog(LogLevel.Info, string.Format("数据库报错：{0}", info), new RunningPlace("Core", "DA_DisConnected"), "DBErr");
            //显示连接中断
            RaiseStatusChangedEvent(new StatusChanged() { Status = ConStatus.DisConnected, Module = FunModule.Db });
        }

        #endregion 事件

        #region 委托

        /// <summary>
        /// 状态变化时间
        /// </summary>
        public event StatusChangedHandler StatusChangedEvent;

        public delegate void StatusChangedHandler(StatusChanged status);

        private void RaiseStatusChangedEvent(StatusChanged status)
        {
            StatusChangedEvent?.Invoke(status);
        }

        #endregion 委托
    }
}