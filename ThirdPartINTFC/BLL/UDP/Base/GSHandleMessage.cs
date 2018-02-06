using LogUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZIT.ThirdPartINTFC.Model;

namespace ZIT.ThirdPartINTFC.BLL.UDP.Base
{
    public class GSHandleMessage
    {
        #region 构造方法
        public GSHandleMessage()
        {
        }

        #endregion 构造方法

        #region 方法

        public void HandleMessage(string message)
        {
            message = GetMessage(message);
            string strMessageId = message.Substring(1, 2);
            switch (strMessageId)
            {
                case "40":
                    Handle40Message(message);
                    break;
                default:
                    break;
            }
        }

        private void Handle40Message(string message)
        {
            try
            {
                VehPosition obj = GetModelFromMsg<VehPosition>(message);
                
                string strCPH = Core.GetInstance().VehMap.ContainsKey(obj.Id) ? Core.GetInstance().VehMap[obj.Id] : string.Empty;
                if (strCPH != string.Empty)
                {
                    string ZLDBH = string.Empty;
                    foreach (var item in Core.GetInstance().BussMap)
                    {
                        if (item.Value.VehList.Contains(strCPH))
                        {
                            ZLDBH = item.Key;
                        }
                        
                    }
                    if (ZLDBH != string.Empty)
                    {

                        message = $"[5216ZLDBH:{ZLDBH}*#JHCCPH:{strCPH}*#XZB:{obj.Jd}*#YZB:{obj.Wd}*#TIME:{obj.Sj}*#EXT1:0*#EXT2:*#EXT3:*#EXT4:*#EXT5:*#]";
                        Handle5216Message(message);
                    }
                }

            }
            catch (Exception ex)
            {
                LogUtility.DataLog.WriteLog(LogLevel.Info, ex.Message, new RunningPlace("HandleMessage", "Handle40Message"), "ComErr");
            }
        }

        private void Handle5216Message(string message)
        {
            JhAmbulanceposition obj = BSMessageHandler.GetModelFromMsg<JhAmbulanceposition>(message);
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


        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private string GetMessage(string message)
        {
            int nBegin = message.IndexOf('(');
            int nEnd = message.IndexOf(')');
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

            Regex reg = new Regex(key + ":(.*?)\\%");
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
