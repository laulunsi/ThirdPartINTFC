using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZIT.ThirdPartINTFC.Model
{
    /// <summary>
    /// 救护车的派车信息
    /// </summary>
    public class JH_AMBULANCEINFO
    {
        private string zldbh;

        private string jhccph;

        private string ssjg;

        private string lxdh;

        private string jsyxm;

        private string ysxm;

        private string ysdh;

        private string gpsstatus;

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
        /// 救护车车牌号
        /// </summary>
        public string JHCCPH { get => jhccph; set => jhccph = value; }

        /// <summary>
        /// 所属机构
        /// </summary>
        public string SSJG { get => ssjg; set => ssjg = value; }

        /// <summary>
        /// 所属机构的联系电话
        /// </summary>
        public string LXDH { get => lxdh; set => lxdh = value; }

        /// <summary>
        /// 驾驶员姓名
        /// </summary>
        public string JSYXM { get => jsyxm; set => jsyxm = value; }

        /// <summary>
        /// 医生姓名
        /// </summary>
        public string YSXM { get => ysxm; set => ysxm = value; }

        /// <summary>
        /// 医生联系电话
        /// </summary>
        public string YSDH { get => ysdh; set => ysdh = value; }

        /// <summary>
        /// 车载GPS状态
        /// </summary>
        public string GPSSTATUS { get => gpsstatus; set => gpsstatus = value; }
        
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
