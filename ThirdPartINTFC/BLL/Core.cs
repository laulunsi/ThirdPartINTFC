using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        #endregion

        #region 构造函数

        public Core()
        {
            
        }



        #endregion

        #region 方法
        public void Start()
        {
            //获取当前未完成的任务
            BussMap = (List<Business>)InfoBll.Get_BUSSINFO();
            

        }


        #endregion



    }
}
