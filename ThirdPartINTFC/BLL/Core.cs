using LogUtility;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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
        public ConcurrentDictionary<string, Business> BussMap;

        /// <summary>
        /// Core实例
        /// </summary>
        private static Core _instance = null;

        public static bool Flag;

        #endregion 变量

        #region 构造函数

        public Core()
        {
            //获取当前未完成的任务
            GetBussMap();
            //初始化业务服务器连接
            Bs = new BssClient()
            {
                LocalPort = SysParameters.BssLocalPort,
                RemoteIpep = new IPEndPoint(IPAddress.Parse(SysParameters.BssServerIp), SysParameters.BssServerPort)
            };
            Bs.HandShakeMsg =
                $"[3000DWBH:{SysParameters.LocalUnitCode}*#DWMC:*#ZJM:{GetZJM()}*#TLX:TFC*#TH:1*#ZBY:*#ZT:1*#LSH:*#ZBBC:*#]";
            Bs.Client.Connected += BSClient_Connected;
            Bs.Client.DisConnected += BSClient_DisConnected;
            //初始化GPS业务服务器连接
            Gs = new GServer() { LocalPort = SysParameters.GpsLocalPort };
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
            if (_instance == null)
            {
                _instance = new Core();
            }
            return _instance;
        }

        /// <summary>
        /// 开始
        /// </summary>
        public void Start()
        {
            try
            {
                Flag = true;
                Bs.Start();
                Gs.Start();
                DA.Start();
                ThreadPool.QueueUserWorkItem(CheckBussMap_Timeout);
            }
            catch (Exception e)
            {
                LogUtility.DataLog.WriteLog(LogLevel.Info, e.Message, new RunningPlace("Core", "Start"), "UdpErr");
            }
        }

        private void CheckBussMap_Timeout(object state)
        {
            while (Flag)
            {
                BussMap = new ConcurrentDictionary<string, Business>(BussMap.Where(p => (DateTime.Now - p.Value.CreateTime).TotalHours < 24).ToDictionary(key => key.Key, business => business.Value));
                Thread.Sleep(1000 * 600);
            }
        }

        public void Stop()
        {
            try
            {
                Flag = false;
                Bs.Client.Connected -= BSClient_Connected;
                Bs.Client.DisConnected -= BSClient_DisConnected;
                Gs.Client.Connected -= GSClient_Connected;
                Gs.Client.DisConnected -= GSClient_DisConnected;

                Bs.Close();
                Gs.Close();
            }
            catch (Exception e)
            {
                LogUtility.DataLog.WriteLog(LogLevel.Info, e.Message, new RunningPlace("Core", "Start"), "UdpErr");
            }
        }

        /// <summary>
        /// 获取业务信息
        /// </summary>
        private void GetBussMap()
        {
            BussMap = new ConcurrentDictionary<string, Business>();
            foreach (var itemBusiness in (List<Business>)InfoBll.Get_BUSSINFO())
            {
                BussMap.TryGetValue(itemBusiness.Zldbh, out Business bus);
                if (bus != null)
                {
                    bus.VehList.Add(itemBusiness.Jhccph);
                    BussMap.AddOrUpdate(itemBusiness.Zldbh, bus, (k, v) => bus);
                }
                else
                {
                    itemBusiness.VehList.Add(itemBusiness.Jhccph);
                    BussMap.AddOrUpdate(itemBusiness.Zldbh, itemBusiness, (k, v) => itemBusiness);
                }
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

        public static void test()
        {
            JhSigninfo jhSigninfo = new JhSigninfo() { Zldbh = "2017121900001", Qsdw = "南京市120", Qsr = "李四", Qssj = DateTime.Now.ToString(CultureInfo.InvariantCulture), Ext1 = "0" };
            JhChargeback jhChargeback = new JhChargeback() { Zldbh = "2017121900001", Tdbh = "201712190002", Tddw = "南京市120", Tdr = "李四", Tdsj = DateTime.Now.ToString(CultureInfo.InvariantCulture), Tdyy = "假警", Ext1 = "0" };
            JhFeedback jhFeedback = new JhFeedback()
            {
                Zldbh = "2017121900001",
                Fkdbh = "201712190002",
                Fkdw = "南京市120",
                Fkr = "李四",
                Fksj = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                Fknr = "这是反馈内容",
                Fkjqlb = "过程反馈",
                Ext1 = "0"
            };
            JhAmbulanceinfo jhAmbulanceinfo = new JhAmbulanceinfo() { Zldbh = "2017121900001", Jhccph = "苏A88888", Ssjg = "南京市120", Lxdh = "13333333333", Jsyxm = "赵四", Ysxm = "徐某", Ysdh = "13344444444", Gpsstatus = "1", Ext1 = "0" };
            JhAmbulancestatus jhAmbulancestatus = new JhAmbulancestatus() { Zldbh = "2017121900001", Jhccph = "苏A88888", Status = "出车", Checi = "01", Time = DateTime.Now.ToString(CultureInfo.InvariantCulture), Ext1 = "0" };
            LogUtility.DataLog.WriteLog(LogLevel.Info, StringHelper.CombinMsg<JhSigninfo>("5211", jhSigninfo), new RunningPlace("", ""), "Test");
            LogUtility.DataLog.WriteLog(LogLevel.Info, StringHelper.CombinMsg<JhChargeback>("5212", jhChargeback), new RunningPlace("", ""), "Test");
            LogUtility.DataLog.WriteLog(LogLevel.Info, StringHelper.CombinMsg<JhFeedback>("5214", jhFeedback), new RunningPlace("", ""), "Test");
            LogUtility.DataLog.WriteLog(LogLevel.Info, StringHelper.CombinMsg<JhAmbulanceinfo>("5215", jhAmbulanceinfo), new RunningPlace("", ""), "Test");
            LogUtility.DataLog.WriteLog(LogLevel.Info, StringHelper.CombinMsg<JhAmbulancestatus>("5217", jhAmbulancestatus), new RunningPlace("", ""), "Test");
            StringBuilder st = new StringBuilder("\r\n");
            foreach (var item in Core.GetInstance().BussMap.ToList())
            {
                st.Append($"指令单编号：{item.Value.Zldbh}\r\n");
                foreach (var Veh in item.Value.VehList)
                {
                    st.Append($"    车牌号：{Veh}\r\n");
                }
            }
            LogUtility.DataLog.WriteLog(LogLevel.Info, st.ToString(), new RunningPlace("", ""), "BussMap");
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