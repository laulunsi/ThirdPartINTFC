using System;
using System.Collections.Generic;

namespace ZIT.ThirdPartINTFC.Model
{
    public class Business
    {
        private string _zldbh;

        private DateTime _createTime;

        private string _zt;

        private List<string> _vehList;

        private string _jhccph;

        private string _lsh;

        #region 构造函数

        public Business()
        {
            _zt = "10";
            VehList = new List<string>();
        }

        public Business(string zldbh)
        {
            _zldbh = zldbh;
            _zt = "10";
            _createTime = DateTime.Now;
            VehList = new List<string>();
        }

        public Business(string zldbh, DateTime creatTime)
        {
            _zldbh = zldbh;
            _zt = "10";
            _createTime = creatTime;
            VehList = new List<string>();
        }

        #endregion 构造函数

        /// <summary>
        /// 指令单编号
        /// </summary>
        public string Zldbh { get => _zldbh; set => _zldbh = value; }

        /// <summary>
        /// 记录创建时间
        /// </summary>
        public DateTime CreateTime { get => _createTime; set => _createTime = value; }

        /// <summary>
        /// 业务处理的状态
        /// 10创建事务/读取工单信息
        /// 20签收工单  21退单
        /// 30处置回馈
        /// 40派车
        /// 50完成
        /// 51退单反馈
        /// 52结果反馈
        /// </summary>
        public string Zt { get => _zt; set => _zt = value; }

        /// <summary>
        /// 车辆列表存储车辆ID
        /// </summary>
        public List<string> VehList { get => _vehList; set => _vehList = value; }

        public string Jhccph { get => _jhccph; set => _jhccph = value; }

        /// <summary>
        /// 120调度系统流水号
        /// </summary>
        public string Lsh { get => _lsh; set => _lsh = value; }
    }
}