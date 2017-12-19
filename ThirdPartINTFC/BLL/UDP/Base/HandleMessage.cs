using LogUtility;
using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using ZIT.ThirdPartINTFC.Model;
using ZIT.ThirdPartINTFC.Utils;

namespace ZIT.ThirdPartINTFC.BLL.UDP.Base
{
    public class MessageHandler
    {
        #region 构造方法

        public MessageHandler()
        {
        }

        #endregion 构造方法

        #region 方法

        public void HandleMessage(string message)
        {
            message = GetMessage(message);
            string strMessageId = message.Substring(1, 4);
            switch (strMessageId)
            {
                case "5211":
                    Handle5211Message(message);
                    break;

                case "5212":
                    Handle5212Message(message);
                    break;

                case "5214":
                    Handle5214Message(message);
                    break;

                case "5215":
                    Handle5215Message(message);
                    break;

                case "5216":
                    Handle5216Message(message);
                    break;

                case "5217":
                    Handle5217Message(message);
                    break;

                default:
                    break;
            }
        }

        private void Handle5211Message(string message)
        {
            JhSigninfo obj = GetModelFromMsg<JhSigninfo>(message);
            if (InfoBll.Update_SIGNINFO(obj))
            {
                Core.GetInstance().Bs.SendMsg(StringHelper.CombinMsg<ResultInfo>("5218", new ResultInfo() { Result = 1, Reason = "插入签收信息成", Type = MsgType.JhSigninfo }));
                LogUtility.DataLog.WriteLog(LogLevel.Info, $"插入签收信息成功，编号：{obj.Zldbh}", new RunningPlace("HandleMessage", "Handle5211Message"), "Running");
                //签收成功，更正状态20
                Core.GetInstance().BussMap.TryGetValue(obj.Zldbh, out Business bus);
                if (bus != null)
                {
                    bus.Zt = "20";
                    Core.GetInstance().BussMap.TryUpdate(obj.Zldbh, bus, bus);
                    InfoBll.Update_WORKORDER(obj.Zldbh, "20");
                }
            }
            else
            {
                Core.GetInstance().Bs.SendMsg(StringHelper.CombinMsg<ResultInfo>("5218", new ResultInfo() { Result = 0, Reason = "插入签收信息失败", Type = MsgType.JhSigninfo }));
                LogUtility.DataLog.WriteLog(LogLevel.Info, $"插入签收信息失败，编号：{obj.Zldbh}", new RunningPlace("HandleMessage", "Handle5211Message"), "Running");
            }
        }

        private void Handle5212Message(string message)
        {
            JhChargeback obj = GetModelFromMsg<JhChargeback>(message);
            if (InfoBll.Update_CHARGEBACK(obj))
            {
                Core.GetInstance().Bs.SendMsg(StringHelper.CombinMsg<ResultInfo>("5218", new ResultInfo() { Result = 1, Reason = "插入退单信息成功", Type = MsgType.JhChargeback }));
                LogUtility.DataLog.WriteLog(LogLevel.Info, $"插入退单信息成功，编号：{obj.Zldbh}", new RunningPlace("HandleMessage", "Handle5212Message"), "Running");
                //退单成功，更正状态21
                Core.GetInstance().BussMap.TryGetValue(obj.Zldbh, out Business bus);
                if (bus != null)
                {
                    bus.Zt = "21";
                    Core.GetInstance().BussMap.TryUpdate(obj.Zldbh, bus, bus);
                    InfoBll.Update_WORKORDER(obj.Zldbh, "21");
                }
            }
            else
            {
                Core.GetInstance().Bs.SendMsg(StringHelper.CombinMsg<ResultInfo>("5218", new ResultInfo() { Result = 0, Reason = "插入退单信息失败", Type = MsgType.JhChargeback }));
                LogUtility.DataLog.WriteLog(LogLevel.Info, $"插入退单信息失败，编号：{obj.Zldbh}", new RunningPlace("HandleMessage", "Handle5212Message"), "Running");
            }
        }

        private void Handle5214Message(string message)
        {
            JhFeedback obj = GetModelFromMsg<JhFeedback>(message);
            if (InfoBll.Update_FEEDBACK(obj))
            {
                Core.GetInstance().Bs.SendMsg(StringHelper.CombinMsg<ResultInfo>("5218", new ResultInfo() { Result = 1, Reason = "插入工单处置信息成功", Type = MsgType.JhFeedback }));
                LogUtility.DataLog.WriteLog(LogLevel.Info, $"插入工单处置信息成功，编号：{obj.Zldbh}", new RunningPlace("HandleMessage", "Handle5214Message"), "Running");
                //工单处置信息成功，更正状态30
                Core.GetInstance().BussMap.TryGetValue(obj.Zldbh, out Business bus);
                if (bus != null)
                {
                    bus.Zt = "30";
                    Core.GetInstance().BussMap.TryUpdate(obj.Zldbh, bus, bus);
                    InfoBll.Update_WORKORDER(obj.Zldbh, "30");
                }
            }
            else
            {
                Core.GetInstance().Bs.SendMsg(StringHelper.CombinMsg<ResultInfo>("5218", new ResultInfo() { Result = 0, Reason = "插入工单处置信息失败", Type = MsgType.JhFeedback }));
                LogUtility.DataLog.WriteLog(LogLevel.Info, $"插入工单处置信息失败，编号：{obj.Zldbh}", new RunningPlace("HandleMessage", "Handle5214Message"), "Running");
            }
        }

        private void Handle5215Message(string message)
        {
            JhAmbulanceinfo obj = GetModelFromMsg<JhAmbulanceinfo>(message);
            if (InfoBll.Update_AMBULANCEINFO(obj))
            {
                Core.GetInstance().Bs.SendMsg(StringHelper.CombinMsg<ResultInfo>("5218", new ResultInfo() { Result = 1, Reason = "插入派车信息成功", Type = MsgType.JhAmbulanceinfo }));
                LogUtility.DataLog.WriteLog(LogLevel.Info, $"插入派车信息成功，编号：{obj.Zldbh}，车辆编号：{obj.Jhccph}", new RunningPlace("HandleMessage", "Handle5215Message"), "Running");
                //派车信息成功，更正状态40，同时增加车辆
                Core.GetInstance().BussMap.TryGetValue(obj.Zldbh, out Business bus);
                if (bus != null)
                {
                    bus.Zt = "40";
                    bus.VehList.Add(obj.Jhccph);
                    Core.GetInstance().BussMap.TryUpdate(obj.Zldbh, bus, bus);
                    InfoBll.Update_WORKORDER(obj.Zldbh, "40");
                }
                else
                {
                }
            }
            else
            {
                Core.GetInstance().Bs.SendMsg(StringHelper.CombinMsg<ResultInfo>("5218", new ResultInfo() { Result = 0, Reason = "插入派车信息成功", Type = MsgType.JhAmbulanceinfo }));
                LogUtility.DataLog.WriteLog(LogLevel.Info, $"插入派车信息失败，编号：{obj.Zldbh}，车辆编号：{obj.Jhccph}", new RunningPlace("HandleMessage", "Handle5215Message"), "Running");
            }
        }

        private void Handle5216Message(string message)
        {
            JhAmbulanceposition obj = GetModelFromMsg<JhAmbulanceposition>(message);
            if (Core.GetInstance().BussMap.Where(p => p.Value.VehList.Contains(obj.Jhccph)).ToList().Count > 0)
            {
                if (InfoBll.Update_AMBULANCEPOSITION(obj))
                {
                    LogUtility.DataLog.WriteLog(LogLevel.Info, $"插入车辆定位信息成功，编号：{obj.Zldbh}，车辆编号：{obj.Jhccph}", new RunningPlace("HandleMessage", "Handle5216Message"), "Running");
                }
                else
                {
                    LogUtility.DataLog.WriteLog(LogLevel.Info, $"插入车辆定位信息失败，编号：{obj.Zldbh}，车辆编号：{obj.Jhccph}", new RunningPlace("HandleMessage", "Handle5216Message"), "Running");
                }
            }
        }

        private void Handle5217Message(string message)
        {
            JhAmbulancestatus obj = GetModelFromMsg<JhAmbulancestatus>(message);
            if (obj.Status == "任务完成" || obj.Status == "任务中止")
            {
                Core.GetInstance().BussMap.TryGetValue(obj.Zldbh, out Business bus);
                if (bus != null)
                {
                    bus.VehList.RemoveAll(p => p == obj.Jhccph);
                    if (bus.VehList.Count == 0)
                    {
                        Core.GetInstance().BussMap.TryRemove(obj.Zldbh, out Business _bus);
                        InfoBll.Update_WORKORDER(obj.Zldbh, "50");
                    }
                }
            }
            if (InfoBll.Update_AMBULANCESTATUS(obj))
            {
                LogUtility.DataLog.WriteLog(LogLevel.Info, $"插入车辆节点信息成功，编号：{obj.Zldbh}，车辆编号：{obj.Jhccph}", new RunningPlace("HandleMessage", "Handle5217Message"), "Running");
            }
            else
            {
                LogUtility.DataLog.WriteLog(LogLevel.Info, $"插入车辆节点信息失败，编号：{obj.Zldbh}，车辆编号：{obj.Jhccph}", new RunningPlace("HandleMessage", "Handle5217Message"), "Running");
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private string GetMessage(string message)
        {
            int nBegin = message.IndexOf('[');
            int nEnd = message.IndexOf(']');
            if (nEnd > nBegin + 1 && nBegin >= 0)
            {
                return message.Substring(nBegin, nEnd - nBegin + 1);
            }
            else
            {
                return string.Empty;
            }
        }

        private T GetModelFromMsg<T>(string message)
        {
            T obj = default(T);
            try
            {
                Type type = typeof(T);
                obj = (T)Activator.CreateInstance(type);
                PropertyInfo[] propertyInfos = type.GetProperties();
                foreach (var property in propertyInfos)
                {
                    property.SetValue(obj, Convert.ToString(GetValueByKey(message, property.Name.ToUpper())));
                }
            }
            catch (Exception e)
            {
                LogUtility.DataLog.WriteLog(LogLevel.Info, e.Message, new RunningPlace("HandleMessage", "GetModelFromMsg"), "ComErr");
            }
            return obj;
        }

        private string GetValueByKey(string message, string key)
        {
            string strReturn = "";

            Regex reg = new Regex(key + ":(.*?)\\*#");
            if (reg.IsMatch(message))
            {
                Match match = reg.Match(message);
                strReturn = match.Groups[1].Value.Trim();
            }
            return strReturn;
        }

        #endregion 方法
    }
}