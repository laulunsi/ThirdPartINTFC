using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZIT.ThirdPartINTFC.Model
{
    /// <summary>
    /// 救护车的出车信息
    /// </summary>
    public class JhAmbulancestatus
    {
        private string _zldbh;

        private string _jhccph;

        private string _status;

        private string _checi;

        private string _time;

        private string _rwzzyy;

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
        /// 救护车状态
        /// </summary>
        public string Status { get => _status; set => _status = value; }

        /// <summary>
        /// 车次
        /// </summary>
        public string Checi { get => _checi; set => _checi = value; }

        /// <summary>
        /// 时间
        /// </summary>
        public string Time { get => _time; set => _time = value; }

        /// <summary>
        /// 任务终止原因
        /// </summary>
        public string Rwzzyy { get => _rwzzyy; set => _rwzzyy = value; }

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
