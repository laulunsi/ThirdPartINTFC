using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZIT.ThirdPartINTFC.Model
{
    /// <summary>
    /// 救护车的派车信息
    /// </summary>
    public class JhAmbulanceinfo
    {
        private string _zldbh;

        private string _jhccph;

        private string _ssjg;

        private string _lxdh;

        private string _jsyxm;

        private string _ysxm;

        private string _ysdh;

        private string _gpsstatus;

        private string _ext1;

        private string _ext2;

        private string _ext3;

        private string _ext4;

        private string _ext5;

        /// <summary>
        /// 指令单编号
        /// </summary>
        public string Zldbh { get => _zldbh; set => _zldbh = value; }

        /// <summary>
        /// 救护车车牌号
        /// </summary>
        public string Jhccph { get => _jhccph; set => _jhccph = value; }

        /// <summary>
        /// 所属机构
        /// </summary>
        public string Ssjg { get => _ssjg; set => _ssjg = value; }

        /// <summary>
        /// 所属机构的联系电话
        /// </summary>
        public string Lxdh { get => _lxdh; set => _lxdh = value; }

        /// <summary>
        /// 驾驶员姓名
        /// </summary>
        public string Jsyxm { get => _jsyxm; set => _jsyxm = value; }

        /// <summary>
        /// 医生姓名
        /// </summary>
        public string Ysxm { get => _ysxm; set => _ysxm = value; }

        /// <summary>
        /// 医生联系电话
        /// </summary>
        public string Ysdh { get => _ysdh; set => _ysdh = value; }

        /// <summary>
        /// 车载GPS状态
        /// </summary>
        public string Gpsstatus { get => _gpsstatus; set => _gpsstatus = value; }
        
        /// <summary>
        /// 冗余字段1
        /// </summary>
        public string Ext1 { get => _ext1; set => _ext1 = value; }

        /// <summary>
        /// 冗余字段2
        /// </summary>
        public string Ext2 { get => _ext2; set => _ext2 = value; }

        /// <summary>
        /// 冗余字段3
        /// </summary>
        public string Ext3 { get => _ext3; set => _ext3 = value; }

        /// <summary>
        /// 冗余字段4
        /// </summary>
        public string Ext4 { get => _ext4; set => _ext4 = value; }

        /// <summary>
        /// 冗余字段5
        /// </summary>
        public string Ext5 { get => _ext5; set => _ext5 = value; }

    }
}
