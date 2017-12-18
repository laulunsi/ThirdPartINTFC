using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using LogUtility;
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
        #endregion

        #region 构造函数

        public DataAnalysis()
        {
            blnConnnect = false;
        }

        #endregion

        #region 方法

        public void Start()
        {
            ThreadPool.QueueUserWorkItem(ConTest);
            ThreadPool.QueueUserWorkItem(Get_JHInfo);
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
                Thread.Sleep(60*1000);
            }
        }

        private void Get_JHInfo(object state)
        {
            while (true)
            {
                try
                {
                    List<JhWorkorder> lst1 = (List<JhWorkorder>) InfoBll.Get_WORKORDER();
                    foreach (var item in lst1)
                    {
                        HandleDBInfo<JhWorkorder>(item);
                    }
                    List<JhChargebackresult> lst2 = (List<JhChargebackresult>)InfoBll.Get_CHARGEBACKRESULT();
                    foreach (var item in lst2)
                    {
                        HandleDBInfo<JhChargebackresult>(item);
                    }
                }
                catch (Exception e)
                {
                    LogUtility.DataLog.WriteLog(LogLevel.Info, e.Message,
                        new RunningPlace("DataAnalysis", "Get_JHInfo"), "DBErr");
                }
                Thread.Sleep(SysParameters.QueryInterval*1000);
            }
        }

        private void HandleDBInfo<T>(object item)
        {
           
            try
            {
                string MsgNo;
                StringBuilder message = new StringBuilder();
                item = (T) item;
                Type type = item.GetType();
                PropertyInfo[] propertyInfos = type.GetProperties();
                foreach (var property in propertyInfos)
                {
                    message.Append($"{property.Name.ToUpper()}:{Convert.ToString(property.GetValue(item))}*#");
                }

                if (item is JhWorkorder)
                {
                    MsgNo = "5210";
                }
                else if (item is JhChargebackresult)
                {
                    MsgNo = "5213";
                }
                else
                {
                    return;
                }
                Core.GetInstance().Bs.SendMsg($"[{MsgNo}{message.ToString()}]");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        #endregion

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


        #endregion

    }
}