using LogUtility;
using System;
using System.Collections.Generic;
using System.Threading;
using ZIT.ThirdPartINTFC.Model;
using ZIT.ThirdPartINTFC.Utils;

namespace ZIT.ThirdPartINTFC.BLL.DA
{
    public class DataAnalysis
    {
        #region 变量

        /// <summary>
        /// 当前连接状态
        /// </summary>
        private static bool blnConnnect;

        #endregion 变量

        #region 构造函数

        public DataAnalysis()
        {
            blnConnnect = false;
        }

        #endregion 构造函数

        #region 方法

        public void Start()
        {
            ThreadPool.QueueUserWorkItem(ConTest);
            ThreadPool.QueueUserWorkItem(Get_JHInfo);
            LogUtility.DataLog.WriteLog(LogLevel.Info, "DataAnalysis已启动", new RunningPlace("DataAnalysis", "Start"), "OP");
        }

        private void ConTest(object state)
        {
            while (true)
            {
                if (InfoBll.ConnectTest())
                {
                    if (!blnConnnect)
                    {
                        RaiseConnected();
                    }
                }
                else
                {
                    if (blnConnnect)
                    {
                        RaiseDisConnected("连接数据库失败");
                    }
                }
                Thread.Sleep(60 * 1000);
            }
        }

        private void Get_JHInfo(object state)
        {
            while (true)
            {
                if (blnConnnect)
                {
                    try
                    {
                        List<JhWorkorder> lst1 = (List<JhWorkorder>)InfoBll.Get_WORKORDER();
                        foreach (var item in lst1)
                        {
                            Core.GetInstance().Bs.SendMsg(StringHelper.CombinMsg<JhWorkorder>("5210", item));
                            LogUtility.DataLog.WriteLog(LogLevel.Info, $"获取工单信息成功，编号：{item.Zldbh}", new RunningPlace("DataAnalysis", "Get_JHInfo"), "Running");
                        }
                        List<JhChargebackresult> lst2 = (List<JhChargebackresult>)InfoBll.Get_CHARGEBACKRESULT();
                        foreach (var item in lst2)
                        {
                            Core.GetInstance().Bs.SendMsg(StringHelper.CombinMsg<JhChargebackresult>("5213", item));
                            LogUtility.DataLog.WriteLog(LogLevel.Info, $"获取退单反馈意见信息成功，编号：{item.Zldbh}", new RunningPlace("DataAnalysis", "Get_JHInfo"), "Running");
                        }
                    }
                    catch (Exception e)
                    {
                        LogUtility.DataLog.WriteLog(LogLevel.Info, e.Message,
                                                    new RunningPlace("DataAnalysis", "Get_JHInfo"), "DBErr");
                    }
                }
                Thread.Sleep(SysParameters.QueryInterval * 1000);
            }
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

        #endregion 委托
    }
}