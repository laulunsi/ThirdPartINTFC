using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZIT.ThirdPartINTFC.Model
{
    /// <summary>
    /// 工单退单
    /// </summary>
    public class JhChargeback
    {
        private string _zldbh;

        private string _tdbh;

        private string _tddw;

        private string _tdr;

        private string _tdsj;

        private string _tdyy;

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
        /// 退单编号
        /// </summary>
        public string Tdbh { get => _tdbh; set => _tdbh = value; }

        /// <summary>
        /// 退单单位代码
        /// </summary>
        public string Tddw { get => _tddw; set => _tddw = value; }

        /// <summary>
        /// 退单人名
        /// </summary>
        public string Tdr { get => _tdr; set => _tdr = value; }

        /// <summary>
        /// 退单时间
        /// </summary>
        public string Tdsj { get => _tdsj; set => _tdsj = value; }

        /// <summary>
        /// 退单原因
        /// </summary>
        public string Tdyy { get => _tdyy; set => _tdyy = value; }
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
