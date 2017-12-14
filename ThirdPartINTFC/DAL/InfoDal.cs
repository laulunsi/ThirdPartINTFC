using System.Collections.Generic;
using ZIT.ThirdPartINTFC.Model;
using ZIT.ThirdPartINTFC.Utils;
using System.Data;
using System.Data.OracleClient;

namespace ZIT.ThirdPartINTFC.DAL
{
    public class InfoDal
    {
        public static bool ConnectTest()
        {
            return OracleHelper.ExecuteTest();
        }

        public static IList<JhWorkorder> Get_WORKORDER()
        {
            return OracleHelper.ExecuteList<JhWorkorder>("SELECT * FROM V_WORKORDER", CommandType.Text, null);
        }

        public static IList<JhChargebackresult> Get_CHARGEBACKRESULT()
        {
            return OracleHelper.ExecuteList<JhChargebackresult>("SELECT * FROM V_CHARGEBACKRESULT", CommandType.Text, null);
        }

        public static bool Update_SIGNINFO(JhSigninfo obj)
        {
            bool blnSuccess = false;
            OracleParameter param1 = new OracleParameter("ZLDBH", OracleType.VarChar) { Value = obj.Zldbh };
            OracleParameter param2 = new OracleParameter("QSDW", OracleType.VarChar) { Value = obj.Qsdw };
            OracleParameter param3 = new OracleParameter("QSR", OracleType.VarChar) { Value = obj.Qsr };
            OracleParameter param4 = new OracleParameter("QSSJ", OracleType.VarChar) { Value = obj.Qssj };
            OracleParameter param5 = new OracleParameter("EXT1", OracleType.VarChar) { Value = obj.Ext1 };
            OracleParameter param6 = new OracleParameter("EXT2", OracleType.VarChar) { Value = obj.Ext2 };
            OracleParameter param7 = new OracleParameter("EXT3", OracleType.VarChar) { Value = obj.Ext3 };
            OracleParameter param8 = new OracleParameter("EXT4", OracleType.VarChar) { Value = obj.Ext4 };
            OracleParameter param9 = new OracleParameter("EXT5", OracleType.VarChar) { Value = obj.Ext5 };
            OracleParameter param10 = new OracleParameter("nFLAG", OracleType.Number)
            {
                Value = 0,
                Direction = ParameterDirection.Output
            };
            OracleParameter param11 = new OracleParameter("errMSG", OracleType.VarChar, 4000)
            {
                Value = "",
                Direction = ParameterDirection.Output
            };
            OracleParameter[] param = {param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11};
            int rows = OracleHelper.ExecuteNonQuery("", CommandType.StoredProcedure, param);
            if (rows > 0)
            {
                blnSuccess = true;
            }
            return blnSuccess;
        }

        public static bool Update_CHARGEBACK(JhChargeback obj)
        {
            bool blnSuccess = false;
            OracleParameter param1 = new OracleParameter("ZLDBH", OracleType.VarChar)
            {
                Value = obj.Zldbh
            };
            OracleParameter param2 = new OracleParameter("TDBH", OracleType.VarChar)
            {
                Value = obj.Tdbh
            };
            OracleParameter param3 = new OracleParameter("TDDW", OracleType.VarChar)
            {
                Value = obj.Tddw
            };
            OracleParameter param4 = new OracleParameter("TDR", OracleType.VarChar)
            {
                Value = obj.Tdr
            };
            OracleParameter param5 = new OracleParameter("TDSJ", OracleType.VarChar)
            {
                Value = obj.Tdsj
            };
            OracleParameter param6 = new OracleParameter("TDYY", OracleType.VarChar)
            {
                Value = obj.Tdyy
            };
            OracleParameter param7 = new OracleParameter("EXT1", OracleType.VarChar)
            {
                Value = obj.Ext1
            };
            OracleParameter param8 = new OracleParameter("EXT2", OracleType.VarChar)
            {
                Value = obj.Ext2
            };
            OracleParameter param9 = new OracleParameter("EXT3", OracleType.VarChar)
            {
                Value = obj.Ext3
            };
            OracleParameter param10 = new OracleParameter("EXT4", OracleType.VarChar)
            {
                Value = obj.Ext4
            };
            OracleParameter param11 = new OracleParameter("EXT5", OracleType.VarChar) { Value = obj.Ext5 };
            OracleParameter param12 = new OracleParameter("nFLAG", OracleType.Number)
            {
                Value = 0,
                Direction = ParameterDirection.Output
            };
            OracleParameter param13 = new OracleParameter("errMSG", OracleType.VarChar, 4000)
            {
                Value = "",
                Direction = ParameterDirection.Output
            };
            OracleParameter[] param = {
                param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12,
                param13
            };
            int rows = OracleHelper.ExecuteNonQuery("", CommandType.StoredProcedure, param);
            if (rows > 0)
            {
                blnSuccess = true;
            }
            return blnSuccess;

        }

        public static bool Update_FEEDBACK(JhFeedback obj)
        {
            bool blnSuccess = false;
            OracleParameter param1 = new OracleParameter("ZLDBH", OracleType.VarChar)
            {
                Value = obj.Zldbh
            };
            OracleParameter param2 = new OracleParameter("FKDBH", OracleType.VarChar)
            {
                Value = obj.Fkdbh
            };
            OracleParameter param3 = new OracleParameter("FKDW", OracleType.VarChar)
            {
                Value = obj.Fkdw
            };
            OracleParameter param4 = new OracleParameter("FKR", OracleType.VarChar)
            {
                Value = obj.Fkr
            };
            OracleParameter param5 = new OracleParameter("FKSJ", OracleType.VarChar)
            {
                Value = obj.Fksj
            };
            OracleParameter param6 = new OracleParameter("FKNR", OracleType.VarChar)
            {
                Value = obj.Fknr
            };
            OracleParameter param7 = new OracleParameter("FKJQLB", OracleType.VarChar)
            {
                Value = obj.Fkjqlb
            };
            OracleParameter param8 = new OracleParameter("EXT1", OracleType.VarChar)
            {
                Value = obj.Ext1
            };
            OracleParameter param9 = new OracleParameter("EXT2", OracleType.VarChar)
            {
                Value = obj.Ext2
            };
            OracleParameter param10 = new OracleParameter("EXT3", OracleType.VarChar)
            {
                Value = obj.Ext3
            };
            OracleParameter param11 = new OracleParameter("EXT4", OracleType.VarChar)
            {
                Value = obj.Ext4
            };
            OracleParameter param12 = new OracleParameter("EXT5", OracleType.VarChar)
            {
                Value = obj.Ext5
            };
            OracleParameter param13 = new OracleParameter("nFLAG", OracleType.Number)
            {
                Value = 0,
                Direction = ParameterDirection.Output
            };
            OracleParameter param14 = new OracleParameter("errMSG", OracleType.VarChar, 4000)
            {
                Value = "",
                Direction = ParameterDirection.Output
            };
            OracleParameter[] param = {
                param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12,
                param13, param14
            };
            int rows = OracleHelper.ExecuteNonQuery("", CommandType.StoredProcedure, param);
            if (rows > 0)
            {
                blnSuccess = true;
            }
            return blnSuccess;
        }

        public static bool Update_AMBULANCEINFO(JhAmbulanceinfo obj)
        {
            bool blnSuccess = false;
            OracleParameter param1 = new OracleParameter("ZLDBH", OracleType.VarChar)
            {
                Value = obj.Zldbh
            };
            OracleParameter param2 = new OracleParameter("JHCCPH", OracleType.VarChar)
            {
                Value = obj.Jhccph
            };
            OracleParameter param3 = new OracleParameter("SSJG", OracleType.VarChar)
            {
                Value = obj.Ssjg
            };
            OracleParameter param4 = new OracleParameter("LXDH", OracleType.VarChar)
            {
                Value = obj.Lxdh
            };
            OracleParameter param5 = new OracleParameter("JSYXM", OracleType.VarChar)
            {
                Value = obj.Jsyxm
            };
            OracleParameter param6 = new OracleParameter("YSXM", OracleType.VarChar)
            {
                Value = obj.Ysxm
            };
            OracleParameter param7 = new OracleParameter("YSDH", OracleType.VarChar)
            {
                Value = obj.Ysdh
            };
            OracleParameter param8 = new OracleParameter("GPSSTATUS", OracleType.VarChar)
            {
                Value = obj.Gpsstatus
            };
            OracleParameter param9 = new OracleParameter("EXT1", OracleType.VarChar)
            {
                Value = obj.Ext1
            };
            OracleParameter param10 = new OracleParameter("EXT2", OracleType.VarChar)
            {
                Value = obj.Ext2
            };
            OracleParameter param11 = new OracleParameter("EXT3", OracleType.VarChar)
            {
                Value = obj.Ext3
            };
            OracleParameter param12 = new OracleParameter("EXT4", OracleType.VarChar)
            {
                Value = obj.Ext4
            };
            OracleParameter param13 = new OracleParameter("EXT5", OracleType.VarChar)
            {
                Value = obj.Ext5
            };
            OracleParameter param14 = new OracleParameter("nFLAG", OracleType.Number)
            {
                Value = 0,
                Direction = ParameterDirection.Output
            };
            OracleParameter param15 = new OracleParameter("errMSG", OracleType.VarChar, 4000)
            {
                Value = "",
                Direction = ParameterDirection.Output
            };
            OracleParameter[] param = {
                param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12,
                param13, param14, param15
            };
            int rows = OracleHelper.ExecuteNonQuery("", CommandType.StoredProcedure, param);
            if (rows > 0)
            {
                blnSuccess = true;
            }
            return blnSuccess;
        }

        public static bool Update_AMBULANCEPOSITION(JhAmbulanceposition obj)
        {
            bool blnSuccess = false;
            OracleParameter param1 = new OracleParameter("ZLDBH", OracleType.VarChar)
            {
                Value = obj.Zldbh
            };
            OracleParameter param2 = new OracleParameter("JHCCPH", OracleType.VarChar)
            {
                Value = obj.Jhccph
            };
            OracleParameter param3 = new OracleParameter("XZB", OracleType.VarChar)
            {
                Value = obj.Xzb
            };
            OracleParameter param4 = new OracleParameter("YZB", OracleType.VarChar)
            {
                Value = obj.Yzb
            };
            OracleParameter param5 = new OracleParameter("TIME", OracleType.VarChar)
            {
                Value = obj.Time
            };
            OracleParameter param6 = new OracleParameter("EXT1", OracleType.VarChar)
            {
                Value = obj.Ext1
            };
            OracleParameter param7 = new OracleParameter("EXT2", OracleType.VarChar)
            {
                Value = obj.Ext2
            };
            OracleParameter param8 = new OracleParameter("EXT3", OracleType.VarChar)
            {
                Value = obj.Ext3
            };
            OracleParameter param9 = new OracleParameter("EXT4", OracleType.VarChar)
            {
                Value = obj.Ext4
            };
            OracleParameter param10 = new OracleParameter("EXT5", OracleType.VarChar)
            {
                Value = obj.Ext5
            };
            OracleParameter param11 = new OracleParameter("nFLAG", OracleType.Number)
            {
                Value = 0,
                Direction = ParameterDirection.Output
            };
            OracleParameter param12 = new OracleParameter("errMSG", OracleType.VarChar, 4000)
            {
                Value = "",
                Direction = ParameterDirection.Output
            };
            OracleParameter[] param = {
                param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12
            };
            int rows = OracleHelper.ExecuteNonQuery("", CommandType.StoredProcedure, param);
            if (rows > 0)
            {
                blnSuccess = true;
            }
            return blnSuccess;

        }

        public static bool Update_AMBULANCESTATUS(JhAmbulancestatus obj)
        {
            bool blnSuccess = false;
            OracleParameter param1 = new OracleParameter("ZLDBH", OracleType.VarChar)
            {
                Value = obj.Zldbh
            };
            OracleParameter param2 = new OracleParameter("JHCCPH", OracleType.VarChar)
            {
                Value = obj.Jhccph
            };
            OracleParameter param3 = new OracleParameter("STATUS", OracleType.VarChar)
            {
                Value = obj.Status
            };
            OracleParameter param4 = new OracleParameter("CHECI", OracleType.VarChar)
            {
                Value = obj.Checi
            };
            OracleParameter param5 = new OracleParameter("TIME", OracleType.VarChar)
            {
                Value = obj.Time
            };
            OracleParameter param6 = new OracleParameter("RWZZYY", OracleType.VarChar)
            {
                Value = obj.Rwzzyy
            };
            OracleParameter param7 = new OracleParameter("EXT1", OracleType.VarChar)
            {
                Value = obj.Ext1
            };
            OracleParameter param8 = new OracleParameter("EXT2", OracleType.VarChar)
            {
                Value = obj.Ext2
            };
            OracleParameter param9 = new OracleParameter("EXT3", OracleType.VarChar)
            {
                Value = obj.Ext3
            };
            OracleParameter param10 = new OracleParameter("EXT4", OracleType.VarChar)
            {
                Value = obj.Ext4
            };
            OracleParameter param11 = new OracleParameter("EXT5", OracleType.VarChar)
            {
                Value = obj.Ext5
            };
            OracleParameter param12 = new OracleParameter("nFLAG", OracleType.Number)
            {
                Value = 0,
                Direction = ParameterDirection.Output
            };
            OracleParameter param13 = new OracleParameter("errMSG", OracleType.VarChar, 4000)
            {
                Value = "",
                Direction = ParameterDirection.Output
            };
            OracleParameter[] param = {
                param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12,
                param13
            };
            int rows = OracleHelper.ExecuteNonQuery("", CommandType.StoredProcedure, param);
            if (rows > 0)
            {
                blnSuccess = true;
            }
            return blnSuccess;
        }
    }
}
