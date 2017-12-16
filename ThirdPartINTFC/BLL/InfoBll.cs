using System.Collections.Generic;
using ZIT.ThirdPartINTFC.DAL;
using ZIT.ThirdPartINTFC.Model;

namespace ZIT.ThirdPartINTFC.BLL
{
    public class InfoBll
    {
        #region 方法
        /// <summary>
        /// 获取当前数据库是否可以连接
        /// </summary>
        /// <returns></returns>
        public static bool ConnectTest()
        {
            return InfoDal.ConnectTest();
        }

        public static IList<Business> Get_BUSSINFO()
        {
            return InfoDal.Get_BUSSINFO();
        }

        /// <summary>
        /// 获取工单同步信息
        /// </summary>
        /// <returns></returns>
        public static IList<JhWorkorder> Get_WORKORDER()
        {
            return InfoDal.Get_WORKORDER();
        }

        /// <summary>
        /// 获取工单退单反馈信息
        /// </summary>
        /// <returns></returns>
        public static IList<JhChargebackresult> Get_CHARGEBACKRESULT()
        {
            return InfoDal.Get_CHARGEBACKRESULT();
        }

        /// <summary>
        /// 更新或插入工单签收信息
        /// </summary>
        /// <param name="signinfo"></param>
        /// <returns></returns>
        public static bool Update_SIGNINFO(JhSigninfo signinfo)
        {
            return InfoDal.Update_SIGNINFO(signinfo);
        }

        /// <summary>
        /// 更新或插入工单退单信息
        /// </summary>
        /// <param name="chargeback"></param>
        /// <returns></returns>
        public static bool Update_CHARGEBACK(JhChargeback chargeback)
        {
            return InfoDal.Update_CHARGEBACK(chargeback);
        }

        /// <summary>
        /// 更新或插入工单处置回复信息
        /// </summary>
        /// <param name="feedback"></param>
        /// <returns></returns>
        public static bool Update_FEEDBACK(JhFeedback feedback)
        {
            return InfoDal.Update_FEEDBACK(feedback);
        }

        /// <summary>
        /// 更新或插入救护车的派车信息
        /// </summary>
        /// <param name="ambulanceinfo"></param>
        /// <returns></returns>
        public static bool Update_AMBULANCEINFO(JhAmbulanceinfo ambulanceinfo)
        {
            return InfoDal.Update_AMBULANCEINFO(ambulanceinfo);
        }

        /// <summary>
        /// 更新或插入救护车的位置信息
        /// </summary>
        /// <param name="ambulanceposition"></param>
        /// <returns></returns>
        public static bool Update_AMBULANCEPOSITION(JhAmbulanceposition ambulanceposition)
        {
            return InfoDal.Update_AMBULANCEPOSITION(ambulanceposition);
        }

        /// <summary>
        /// 更新或插入救护车的出车信息
        /// </summary>
        /// <param name="ambulancestatus"></param>
        /// <returns></returns>
        public static bool Update_AMBULANCESTATUS(JhAmbulancestatus ambulancestatus)
        {
            return InfoDal.Update_AMBULANCESTATUS(ambulancestatus);
        }
        #endregion
    }
}
