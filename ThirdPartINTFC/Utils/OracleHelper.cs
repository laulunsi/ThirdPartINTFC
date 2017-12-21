#pragma warning disable 0618

using LogUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Reflection;

namespace ZIT.ThirdPartINTFC.Utils
{
    public class OracleHelper
    {
        #region 变量

        /// <summary>
        /// 数据库连接字符串 "Data Source=orcl;User Id=Nj120;Password=Nj120"
        /// </summary>
        public static string DbConnectStr = SysParameters.DbConnectStr;

        #endregion 变量

        #region 方法

        #region 测试

        /// <summary>
        /// 用于测试数据库连接是否有效
        /// </summary>
        /// <returns></returns>
        public static bool ExecuteTest()
        {
            bool bln = false;
            try
            {
                using (OracleConnection con = new OracleConnection(DbConnectStr))
                {
                    using (OracleCommand cmd = new OracleCommand("SELECT SYSDATE FROM DUAL", con))
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }
                        cmd.CommandType = CommandType.Text;
                        object obj = cmd.ExecuteOracleScalar();
                        if (obj != null && obj != DBNull.Value)
                        {
                            bln = true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                bln = false;
                LogUtility.DataLog.WriteLog(LogLevel.Info, e.Message, new RunningPlace("OracleHelper", "ExecuteTest"), "DBErr");
            }
            return bln;
        }

        #endregion 测试

        #region 执行返回多条记录的泛型集合对象

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
            List<T> list = new List<T>();
            using (OracleConnection con = new OracleConnection(DbConnectStr))
            {
                using (OracleCommand cmd = new OracleCommand(commandText, con))
                {
                    try
                    {
                        cmd.CommandType = commandType;
                        if (param != null)
                        {
                            cmd.Parameters.AddRange(param);
                        }
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }
                        OracleDataReader reader = cmd.ExecuteReader();
                        cmd.Parameters.Clear();

                        while (reader.Read())
                        {
                            T obj = ExecuteDataReader<T>(reader);
                            list.Add(obj);
                        }
                        reader.Close();
                        cmd.Dispose();
                    }
                    catch (Exception e)
                    {
                        cmd.Parameters.Clear();
                        cmd.Dispose();
                        con.Close();
                        LogUtility.DataLog.WriteLog(LogLevel.Info, e.Message, new RunningPlace("OracleHelper", "ExecuteList"), "DBErr");
                    }
                }
            }
            return list;
        }

        #endregion 执行返回多条记录的泛型集合对象

        #region 执行不查询的数据库操作

        /// <summary>
        /// 执行不查询的数据库操作
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string commandText, CommandType commandType, params OracleParameter[] param)
        {
            int result;
            using (OracleConnection con = new OracleConnection(DbConnectStr))
            {
                using (OracleCommand cmd = new OracleCommand(commandText, con))
                {
                    try
                    {
                        cmd.CommandType = commandType;
                        if (param != null)
                        {
                            cmd.Parameters.AddRange(param);
                        }
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }
                        result = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        cmd.Dispose();
                        con.Close();
                    }
                    catch (Exception e)
                    {
                        result = 0;
                        cmd.Parameters.Clear();
                        cmd.Dispose();
                        con.Close();
                        LogUtility.DataLog.WriteLog(LogLevel.Info, e.Message, new RunningPlace("OracleHelper", "ExecuteNonQuery"), "DBErr");
                    }
                }
            }
            return result;
        }

        #endregion 执行不查询的数据库操作

        #region 执行返回一条记录的泛型对象

        /// <summary>
        /// 执行返回一条记录的泛型对象
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="reader">只进只读对象</param>
        /// <returns>泛型对象</returns>
        private static T ExecuteDataReader<T>(OracleDataReader reader)
        {
            T obj = default(T);
            try
            {
                Type type = typeof(T);
                obj = (T)Activator.CreateInstance(type);//从当前程序集里面通过反射的方式创建指定类型的对象
                PropertyInfo[] propertyInfos = type.GetProperties();//获取指定类型里面的所有属性
                foreach (var property in propertyInfos)
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string fieldName = reader.GetName(i);
                        if (fieldName.ToLower() == property.Name.ToLower())
                        {
                            object val = reader[fieldName];//获取记录中对应属性的值
                            if (val != null && val != DBNull.Value)
                            {
                                if (val is decimal)
                                {
                                    property.SetValue(obj, Convert.ToDecimal(val));
                                }
                                if (val is int)
                                {
                                    property.SetValue(obj, Convert.ToInt32(val));
                                }
                                if (val is DateTime)
                                {
                                    property.SetValue(obj, Convert.ToDateTime(val));
                                }
                                if (val is string)
                                {
                                    property.SetValue(obj, Convert.ToString(val));
                                }
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogUtility.DataLog.WriteLog(LogLevel.Info, e.Message, new RunningPlace("OracleHelper", "ExecuteDataReader"), "DBErr");
            }
            return obj;
        }

        #endregion 执行返回一条记录的泛型对象

        #endregion 方法
    }
}