using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZIT.ThirdPartINTFC.Model
{
    /// <summary>
    /// 工单签收
    /// </summary>
    public class JH_SIGNINFO
    {
        private string zldbh;

        private string qsdw;

        private string qsr;

        private string qssj;

        private string ext1;

        private string ext2;

        private string ext3;

        private string ext4;

        private string ext5;

        /// <summary>
        /// 指令单编号
        /// </summary>
        public string ZLDBH { get => zldbh; set => zldbh = value; }

        /// <summary>
        /// 签收单位
        /// </summary>
        public string QSDW { get => qsdw; set => qsdw = value; }

        /// <summary>
        /// 签收人名
        /// </summary>
        public string QSR { get => qsr; set => qsr = value; }

        /// <summary>
        /// 签收时间
        /// </summary>
        public string QSSJ { get => qssj; set => qssj = value; }

        /// <summary>
        /// 冗余字段1
        /// </summary>
        public string EXT1 { get => ext1; set => ext1 = value; }

        /// <summary>
        /// 冗余字段2
        /// </summary>
        public string EXT2 { get => ext2; set => ext2 = value; }

        /// <summary>
        /// 冗余字段3
        /// </summary>
        public string EXT3 { get => ext3; set => ext3 = value; }

        /// <summary>
        /// 冗余字段4
        /// </summary>
        public string EXT4 { get => ext4; set => ext4 = value; }

        /// <summary>
        /// 冗余字段5
        /// </summary>
        public string EXT5 { get => ext5; set => ext5 = value; }
    }
}
