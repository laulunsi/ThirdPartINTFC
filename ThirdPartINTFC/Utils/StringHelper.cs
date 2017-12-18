using System;
using System.Reflection;
using System.Text;

namespace ZIT.ThirdPartINTFC.Utils
{
    public class StringHelper
    {
        public static string CombinMsg<T>(string MsgNo, object item)
        {
            StringBuilder message = new StringBuilder();
            item = (T)item;
            Type type = item.GetType();
            PropertyInfo[] propertyInfos = type.GetProperties();
            foreach (var property in propertyInfos)
            {
                message.Append($"{property.Name.ToUpper()}:{Convert.ToString(property.GetValue(item))}*#");
            }
            return $"[{MsgNo}{message.ToString()}]";
        }
    }
}