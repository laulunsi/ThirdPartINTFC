using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZIT.ThirdPartINTFC.Model
{
    /// <summary>
    /// 退单结果反馈
    /// </summary>
    public class JH_CHARGEBACKRESULT
    {
        private string zldbh;

        private string tdbh;

        private string tdjg;

        private string jjtdly;

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
        /// 退单编号
        /// </summary>
        public string TDBH { get => tdbh; set => tdbh = value; }

        /// <summary>
        /// 退单结果 
        /// 0:接受退单
        /// 1:拒绝退单
        /// </summary>
        public string TDJG { get => tdjg; set => tdjg = value; }

        /// <summary>
        /// 拒绝退单理由
        /// </summary>
        public string JJTDLY { get => jjtdly; set => jjtdly = value; }
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
