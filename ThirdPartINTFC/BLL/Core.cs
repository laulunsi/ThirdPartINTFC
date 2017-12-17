using System.Collections.Generic;
using ZIT.ThirdPartINTFC.Model;

namespace ZIT.ThirdPartINTFC.BLL
{
    public class Core
    {
        #region 变量

        /// <summary>
        /// 业务信息集合
        /// </summary>
        public List<Business> BussMap;

        #endregion 变量

        #region 构造函数

        public Core()
        {
        }

        #endregion 构造函数

        #region 方法

        public void Start()
        {
            //获取当前未完成的任务
            BussMap = (List<Business>)InfoBll.Get_BUSSINFO();
        }

        #endregion 方法
    }
}