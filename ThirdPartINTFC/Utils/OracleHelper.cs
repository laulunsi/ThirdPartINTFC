#pragma warning disable 0618
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZIT.ThirdPartINTFC.Utils
{
    public class OracleHelper
    {
        #region 变量
        public static string DBstr;

        #endregion

        #region 属性



        #endregion

        #region 方法
        /// <summary>
        /// 执行返回多条记录的泛型集合对象
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="commandText">Oracle语句或存储过程名</param>
        /// <param name="commandType">Oracle命令类型</param>
        /// <param name="param">Oracle命令参数数组</param>  using (OracleConnection cc = new OracleConnection((constr_ == "0") ? constr : constrrec))
        /// <returns>泛型集合对象</returns>
        public static List<T> ExecuteList<T>(string commandText, CommandType commandType, params OracleParameter[] param)
        {
            List<T> list=new List<T>();
            using (OracleConnection con = new OracleConnection(DBstr))
            {
                using (OracleCommand cmd = new OracleCommand(commandText,con))
                {
                     
                }

            }
        }


        #endregion

    }
}
