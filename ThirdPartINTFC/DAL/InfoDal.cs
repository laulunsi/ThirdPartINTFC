﻿using LogUtility;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using ZIT.ThirdPartINTFC.Model;
using ZIT.ThirdPartINTFC.Utils;

namespace ZIT.ThirdPartINTFC.DAL
{
    public class InfoDal
    {
        public static bool ConnectTest()
        {
            return OracleHelper.ExecuteTest();
        }

        public static IList<Business> Get_BUSSINFO()
        {
            return OracleHelper.ExecuteList<Business>("select * from V_BUSSINFO", CommandType.Text, null);
        }

        public static IList<JhWorkorder> Get_WORKORDER()
        {
            return OracleHelper.ExecuteList<JhWorkorder>("select * from V_WORKORDER", CommandType.Text, null);
        }

        public static IList<JhChargebackresult> Get_CHARGEBACKRESULT()
        {
            return OracleHelper.ExecuteList<JhChargebackresult>("select * from V_CHARGEBACKRESULT", CommandType.Text, null);
        }

        public static bool Update_WORKORDER(string zldbh, string zt)
        {
            bool blnSuccess = false;
            OracleParameter param1 = new OracleParameter("vc_ZLDBH", OracleType.VarChar) { Value = zldbh };
            OracleParameter param2 = new OracleParameter("vc_ZT", OracleType.VarChar) { Value = zt };
            OracleParameter param3 = new OracleParameter("nFLAG", OracleType.Number)
            { Value = 0, Direction = ParameterDirection.Output };
            OracleParameter param4 = new OracleParameter("errMSG", OracleType.VarChar, 4000)
            { Value = "", Direction = ParameterDirection.Output };
            OracleParameter[] param =
                {param1, param2, param3,param4};
            int rows = OracleHelper.ExecuteNonQuery("ZIT_ITFC.SP_Update_WORKORDER", CommandType.StoredProcedure, param);
            WriteDbExecuteLog(param[2].Value.ToString(), param[3].Value.ToString(), GetMethodName());
            if (rows > 0 && int.Parse(param[2].Value.ToString()) == 1)
            {
                blnSuccess = true;
            }
            return blnSuccess;
        }

        public static bool Update_CHARGEBACKRESULT(string zldbh)
        {
            bool blnSuccess = false;
            OracleParameter param1 = new OracleParameter("vc_ZLDBH", OracleType.VarChar) { Value = zldbh };
            OracleParameter param2 = new OracleParameter("nFLAG", OracleType.Number)
            { Value = 0, Direction = ParameterDirection.Output };
            OracleParameter param3 = new OracleParameter("errMSG", OracleType.VarChar, 4000)
            { Value = "", Direction = ParameterDirection.Output };
            OracleParameter[] param =
                {param1, param2, param3};
            int rows = OracleHelper.ExecuteNonQuery("ZIT_ITFC.SP_Update_CHARGEBACKRESULT", CommandType.StoredProcedure, param);
            WriteDbExecuteLog(param[1].Value.ToString(), param[2].Value.ToString(), GetMethodName());
            if (rows > 0 && int.Parse(param[1].Value.ToString()) == 1)
            {
                blnSuccess = true;
            }
            return blnSuccess;
        }

        public static bool Update_SIGNINFO(JhSigninfo obj)
        {
            bool blnSuccess = false;
            OracleParameter param1 = new OracleParameter("vc_ZLDBH", OracleType.VarChar) { Value = obj.Zldbh };
            OracleParameter param2 = new OracleParameter("vc_QSDW", OracleType.VarChar) { Value = obj.Qsdw };
            OracleParameter param3 = new OracleParameter("vc_QSR", OracleType.VarChar) { Value = obj.Qsr };
            OracleParameter param4 = new OracleParameter("vc_QSSJ", OracleType.VarChar) { Value = obj.Qssj };
            OracleParameter param5 = new OracleParameter("vc_EXT1", OracleType.VarChar) { Value = obj.Ext1 };
            OracleParameter param6 = new OracleParameter("vc_EXT2", OracleType.VarChar) { Value = obj.Ext2 };
            OracleParameter param7 = new OracleParameter("vc_EXT3", OracleType.VarChar) { Value = obj.Ext3 };
            OracleParameter param8 = new OracleParameter("vc_EXT4", OracleType.VarChar) { Value = obj.Ext4 };
            OracleParameter param9 = new OracleParameter("vc_EXT5", OracleType.VarChar) { Value = obj.Ext5 };
            OracleParameter param10 = new OracleParameter("nFLAG", OracleType.Number)
            { Value = 0, Direction = ParameterDirection.Output };
            OracleParameter param11 = new OracleParameter("errMSG", OracleType.VarChar, 4000)
            { Value = "", Direction = ParameterDirection.Output };
            OracleParameter[] param =
                {param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11};
            int rows = OracleHelper.ExecuteNonQuery("ZIT_ITFC.SP_Update_SIGNINFO", CommandType.StoredProcedure, param);
            WriteDbExecuteLog(param[9].Value.ToString(), param[10].Value.ToString(), GetMethodName());
            if (rows > 0 && int.Parse(param[9].Value.ToString()) == 1)
            {
                blnSuccess = true;
            }
            return blnSuccess;
        }

        public static bool Update_CHARGEBACK(JhChargeback obj)
        {
            bool blnSuccess = false;
            OracleParameter param1 = new OracleParameter("vc_ZLDBH", OracleType.VarChar) { Value = obj.Zldbh };
            OracleParameter param2 = new OracleParameter("vc_TDBH", OracleType.VarChar) { Value = obj.Tdbh };
            OracleParameter param3 = new OracleParameter("vc_TDDW", OracleType.VarChar) { Value = obj.Tddw };
            OracleParameter param4 = new OracleParameter("vc_TDR", OracleType.VarChar) { Value = obj.Tdr };
            OracleParameter param5 = new OracleParameter("vc_TDSJ", OracleType.VarChar) { Value = obj.Tdsj };
            OracleParameter param6 = new OracleParameter("vc_TDYY", OracleType.VarChar) { Value = obj.Tdyy };
            OracleParameter param7 = new OracleParameter("vc_EXT1", OracleType.VarChar) { Value = obj.Ext1 };
            OracleParameter param8 = new OracleParameter("vc_EXT2", OracleType.VarChar) { Value = obj.Ext2 };
            OracleParameter param9 = new OracleParameter("vc_EXT3", OracleType.VarChar) { Value = obj.Ext3 };
            OracleParameter param10 = new OracleParameter("vc_EXT4", OracleType.VarChar) { Value = obj.Ext4 };
            OracleParameter param11 = new OracleParameter("vc_EXT5", OracleType.VarChar) { Value = obj.Ext5 };
            OracleParameter param12 = new OracleParameter("nFLAG", OracleType.Number)
            { Value = 0, Direction = ParameterDirection.Output };
            OracleParameter param13 = new OracleParameter("errMSG", OracleType.VarChar, 4000)
            { Value = "", Direction = ParameterDirection.Output };
            OracleParameter[] param =
            {
                param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12,
                param13
            };
            int rows = OracleHelper.ExecuteNonQuery("ZIT_ITFC.SP_Update_CHARGEBACK", CommandType.StoredProcedure, param);
            WriteDbExecuteLog(param[11].Value.ToString(), param[12].Value.ToString(), GetMethodName());
            if (rows > 0 && int.Parse(param[11].Value.ToString()) == 1)
            {
                blnSuccess = true;
            }
            return blnSuccess;
        }

        public static bool Update_FEEDBACK(JhFeedback obj)
        {
            bool blnSuccess = false;
            OracleParameter param1 = new OracleParameter("vc_ZLDBH", OracleType.VarChar) { Value = obj.Zldbh };
            OracleParameter param2 = new OracleParameter("vc_FKDBH", OracleType.VarChar) { Value = obj.Fkdbh };
            OracleParameter param3 = new OracleParameter("vc_FKDW", OracleType.VarChar) { Value = obj.Fkdw };
            OracleParameter param4 = new OracleParameter("vc_FKR", OracleType.VarChar) { Value = obj.Fkr };
            OracleParameter param5 = new OracleParameter("vc_FKSJ", OracleType.VarChar) { Value = obj.Fksj };
            OracleParameter param6 = new OracleParameter("vc_FKNR", OracleType.VarChar) { Value = obj.Fknr };
            OracleParameter param7 = new OracleParameter("vc_FKJQLB", OracleType.VarChar) { Value = obj.Fkjqlb };
            OracleParameter param8 = new OracleParameter("vc_EXT1", OracleType.VarChar) { Value = obj.Ext1 };
            OracleParameter param9 = new OracleParameter("vc_EXT2", OracleType.VarChar) { Value = obj.Ext2 };
            OracleParameter param10 = new OracleParameter("vc_EXT3", OracleType.VarChar) { Value = obj.Ext3 };
            OracleParameter param11 = new OracleParameter("vc_EXT4", OracleType.VarChar) { Value = obj.Ext4 };
            OracleParameter param12 = new OracleParameter("vc_EXT5", OracleType.VarChar) { Value = obj.Ext5 };
            OracleParameter param13 = new OracleParameter("nFLAG", OracleType.Number)
            { Value = 0, Direction = ParameterDirection.Output };
            OracleParameter param14 = new OracleParameter("errMSG", OracleType.VarChar, 4000)
            { Value = "", Direction = ParameterDirection.Output };
            OracleParameter[] param =
            {
                param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12,
                param13, param14
            };
            int rows = OracleHelper.ExecuteNonQuery("ZIT_ITFC.SP_Update_FEEDBACK", CommandType.StoredProcedure, param);
            WriteDbExecuteLog(param[12].Value.ToString(), param[13].Value.ToString(), GetMethodName());
            if (rows > 0 && int.Parse(param[12].Value.ToString()) == 1)
            {
                blnSuccess = true;
            }
            return blnSuccess;
        }

        public static bool Update_AMBULANCEINFO(JhAmbulanceinfo obj)
        {
            bool blnSuccess = false;
            OracleParameter param1 = new OracleParameter("vc_ZLDBH", OracleType.VarChar) { Value = obj.Zldbh };
            OracleParameter param2 = new OracleParameter("vc_JHCCPH", OracleType.VarChar) { Value = obj.Jhccph };
            OracleParameter param3 = new OracleParameter("vc_SSJG", OracleType.VarChar) { Value = obj.Ssjg };
            OracleParameter param4 = new OracleParameter("vc_LXDH", OracleType.VarChar) { Value = obj.Lxdh };
            OracleParameter param5 = new OracleParameter("vc_JSYXM", OracleType.VarChar) { Value = obj.Jsyxm };
            OracleParameter param6 = new OracleParameter("vc_YSXM", OracleType.VarChar) { Value = obj.Ysxm };
            OracleParameter param7 = new OracleParameter("vc_YSDH", OracleType.VarChar) { Value = obj.Ysdh };
            OracleParameter param8 = new OracleParameter("vc_GPSSTATUS", OracleType.VarChar) { Value = obj.Gpsstatus };
            OracleParameter param9 = new OracleParameter("vc_EXT1", OracleType.VarChar) { Value = obj.Ext1 };
            OracleParameter param10 = new OracleParameter("vc_EXT2", OracleType.VarChar) { Value = obj.Ext2 };
            OracleParameter param11 = new OracleParameter("vc_EXT3", OracleType.VarChar) { Value = obj.Ext3 };
            OracleParameter param12 = new OracleParameter("vc_EXT4", OracleType.VarChar) { Value = obj.Ext4 };
            OracleParameter param13 = new OracleParameter("vc_EXT5", OracleType.VarChar) { Value = obj.Ext5 };
            OracleParameter param14 = new OracleParameter("nFLAG", OracleType.Number)
            { Value = 0, Direction = ParameterDirection.Output };
            OracleParameter param15 = new OracleParameter("errMSG", OracleType.VarChar, 4000)
            { Value = "", Direction = ParameterDirection.Output };
            OracleParameter[] param =
            {
                param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12,
                param13, param14, param15
            };
            int rows = OracleHelper.ExecuteNonQuery("ZIT_ITFC.SP_Update_AMBULANCEINFO", CommandType.StoredProcedure, param);
            WriteDbExecuteLog(param[13].Value.ToString(), param[14].Value.ToString(), GetMethodName());
            if (rows > 0 && int.Parse(param[13].Value.ToString()) == 1)
            {
                blnSuccess = true;
            }
            return blnSuccess;
        }

        public static bool Update_AMBULANCEPOSITION(JhAmbulanceposition obj)
        {
            bool blnSuccess = false;
            OracleParameter param1 = new OracleParameter("vc_ZLDBH", OracleType.VarChar) { Value = obj.Zldbh };
            OracleParameter param2 = new OracleParameter("vc_JHCCPH", OracleType.VarChar) { Value = obj.Jhccph };
            OracleParameter param3 = new OracleParameter("vc_XZB", OracleType.VarChar) { Value = obj.Xzb };
            OracleParameter param4 = new OracleParameter("vc_YZB", OracleType.VarChar) { Value = obj.Yzb };
            OracleParameter param5 = new OracleParameter("vc_TIME", OracleType.VarChar) { Value = obj.Time };
            OracleParameter param6 = new OracleParameter("vc_EXT1", OracleType.VarChar) { Value = obj.Ext1 };
            OracleParameter param7 = new OracleParameter("vc_EXT2", OracleType.VarChar) { Value = obj.Ext2 };
            OracleParameter param8 = new OracleParameter("vc_EXT3", OracleType.VarChar) { Value = obj.Ext3 };
            OracleParameter param9 = new OracleParameter("vc_EXT4", OracleType.VarChar) { Value = obj.Ext4 };
            OracleParameter param10 = new OracleParameter("vc_EXT5", OracleType.VarChar) { Value = obj.Ext5 };
            OracleParameter param11 = new OracleParameter("nFLAG", OracleType.Number)
            { Value = 0, Direction = ParameterDirection.Output };
            OracleParameter param12 = new OracleParameter("errMSG", OracleType.VarChar, 4000)
            { Value = "", Direction = ParameterDirection.Output };
            OracleParameter[] param =
            {
                param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12
            };
            int rows = OracleHelper.ExecuteNonQuery("ZIT_ITFC.SP_Update_AMBULANCEPOSITION", CommandType.StoredProcedure, param);
            WriteDbExecuteLog(param[10].Value.ToString(), param[11].Value.ToString(), GetMethodName());
            if (rows > 0 && int.Parse(param[10].Value.ToString()) == 1)
            {
                blnSuccess = true;
            }
            return blnSuccess;
        }

        public static bool Update_AMBULANCESTATUS(JhAmbulancestatus obj)
        {
            bool blnSuccess = false;
            OracleParameter param1 = new OracleParameter("vc_ZLDBH", OracleType.VarChar) { Value = obj.Zldbh };
            OracleParameter param2 = new OracleParameter("vc_JHCCPH", OracleType.VarChar) { Value = obj.Jhccph };
            OracleParameter param3 = new OracleParameter("vc_STATUS", OracleType.VarChar) { Value = obj.Status };
            OracleParameter param4 = new OracleParameter("vc_CHECI", OracleType.VarChar) { Value = obj.Checi };
            OracleParameter param5 = new OracleParameter("vc_TIME", OracleType.VarChar) { Value = obj.Time };
            OracleParameter param6 = new OracleParameter("vc_RWZZYY", OracleType.VarChar) { Value = obj.Rwzzyy };
            OracleParameter param7 = new OracleParameter("vc_EXT1", OracleType.VarChar) { Value = obj.Ext1 };
            OracleParameter param8 = new OracleParameter("vc_EXT2", OracleType.VarChar) { Value = obj.Ext2 };
            OracleParameter param9 = new OracleParameter("vc_EXT3", OracleType.VarChar) { Value = obj.Ext3 };
            OracleParameter param10 = new OracleParameter("vc_EXT4", OracleType.VarChar) { Value = obj.Ext4 };
            OracleParameter param11 = new OracleParameter("vc_EXT5", OracleType.VarChar) { Value = obj.Ext5 };
            OracleParameter param12 = new OracleParameter("nFLAG", OracleType.Number)
            { Value = 0, Direction = ParameterDirection.Output };
            OracleParameter param13 = new OracleParameter("errMSG", OracleType.VarChar, 4000)
            { Value = "", Direction = ParameterDirection.Output };
            OracleParameter[] param =
            {
                param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12,
                param13
            };
            int rows = OracleHelper.ExecuteNonQuery("ZIT_ITFC.SP_Update_AMBULANCESTATUS", CommandType.StoredProcedure, param);
            WriteDbExecuteLog(param[11].Value.ToString(), param[12].Value.ToString(), GetMethodName());
            if (rows > 0 && int.Parse(param[11].Value.ToString()) == 1)
            {
                blnSuccess = true;
            }
            return blnSuccess;
        }

        private static void WriteDbExecuteLog(string flag, string errMsg, string fnName)
        {
            if (int.Parse(flag) == 0)
            {
                LogUtility.DataLog.WriteLog(LogLevel.Info, errMsg, new RunningPlace("InfoDAL", fnName), "DbErr");
            }
        }

        public static string GetMethodName()
        {
            var method = new StackFrame(1).GetMethod(); // 这里忽略1层堆栈，也就忽略了当前方法GetMethodName，这样拿到的就正好是外部调用GetMethodName的方法信息
            var property = (
            from p in method.DeclaringType.GetProperties(
            BindingFlags.Instance |
            BindingFlags.Static |
            BindingFlags.Public |
            BindingFlags.NonPublic)
            where p.GetGetMethod(true) == method || p.GetSetMethod(true) == method
            select p).FirstOrDefault();
            return property == null ? method.Name : property.Name;
        }
    }
}