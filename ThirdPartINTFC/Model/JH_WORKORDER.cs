using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZIT.ThirdPartINTFC.Model
{
    /// <summary>
    /// 工单同步
    /// </summary>
    public class JH_WORKORDER
    {
        private string zldbh;

        private string bjr;

        private string xb;

        private string lxdh;

        private string sfdz;

        private string bjsj;

        private string bjnr;

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
        /// 报警人
        /// </summary>
        public string BJR { get => bjr; set => bjr = value; }

        /// <summary>
        /// 性别
        /// </summary>
        public string XB { get => xb; set => xb = value; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string LXDH { get => lxdh; set => lxdh = value; }

        /// <summary>
        /// 事发地址
        /// </summary>
        public string SFDZ { get => sfdz; set => sfdz = value; }

        /// <summary>
        /// 报警时间
        /// </summary>
        public string BJSJ { get => bjsj; set => bjsj = value; }
        
        /// <summary>
        /// 报警内容
        /// </summary>
        public string BJNR { get => bjnr; set => bjnr = value; }

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
