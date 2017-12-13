using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZIT.ThirdPartINTFC.Model
{
    /// <summary>
    /// 工单处置回复
    /// </summary>
    public class JH_FEEDBACK
    {
        private string zldbh;

        private string fkdbh;

        private string fkdw;

        private string fkr;

        private string fksj;

        private string fknr;

        private string fkjqlb;

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
        /// 反馈单编号
        /// </summary>
        public string FKDBH { get => fkdbh; set => fkdbh = value; }

        /// <summary>
        /// 反馈单位
        /// </summary>
        public string FKDW { get => fkdw; set => fkdw = value; }

        /// <summary>
        /// 反馈人
        /// </summary>
        public string FKR { get => fkr; set => fkr = value; }

        /// <summary>
        /// 反馈时间
        /// </summary>
        public string FKSJ { get => fksj; set => fksj = value; }

        /// <summary>
        /// 反馈内容
        /// </summary>
        public string FKNR { get => fknr; set => fknr = value; }

        /// <summary>
        /// 反馈警情类别(过程反馈/结果反馈)
        /// </summary>
        public string FKJQLB { get => fkjqlb; set => fkjqlb = value; }
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
