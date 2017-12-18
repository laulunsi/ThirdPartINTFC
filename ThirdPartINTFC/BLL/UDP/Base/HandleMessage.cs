using System;
using System.Reflection;
using System.Text.RegularExpressions;
using ZIT.ThirdPartINTFC.Model;

namespace ZIT.ThirdPartINTFC.BLL.UDP.Base
{
    public class MessageHandler
    {
        #region 构造方法

        public MessageHandler()
        {
        }

        #endregion


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
            }
            else
            {
            }
        }


        private void Handle5212Message(string message)
        {
            JhChargeback obj = GetModelFromMsg<JhChargeback>(message);
            if (InfoBll.Update_CHARGEBACK(obj))
            {
            }
            else
            {
            }
        }

        private void Handle5214Message(string message)
        {
            JhFeedback obj = GetModelFromMsg<JhFeedback>(message);
            if (InfoBll.Update_FEEDBACK(obj))
            {
            }
            else
            {
            }
        }

        private void Handle5215Message(string message)
        {
            JhAmbulanceinfo obj = GetModelFromMsg<JhAmbulanceinfo>(message);
            if (InfoBll.Update_AMBULANCEINFO(obj))
            {
            }
            else
            {
            }
        }
        private void Handle5216Message(string message)
        {
            JhAmbulanceposition obj = GetModelFromMsg<JhAmbulanceposition>(message);
            if (InfoBll.Update_AMBULANCEPOSITION(obj))
            {
            }
            else
            {
            }
        }
        private void Handle5217Message(string message)
        {
            JhAmbulancestatus obj = GetModelFromMsg<JhAmbulancestatus>(message);
            if (InfoBll.Update_AMBULANCESTATUS(obj))
            {

            }
            else
            {
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
                obj = (T) Activator.CreateInstance(type);
                PropertyInfo[] propertyInfos = type.GetProperties();
                foreach (var property in propertyInfos)
                {
                    property.SetValue(obj, Convert.ToString(GetValueByKey(message, property.Name.ToUpper())));
                }
            }
            catch (Exception e)
            {
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

        #endregion
    }
}