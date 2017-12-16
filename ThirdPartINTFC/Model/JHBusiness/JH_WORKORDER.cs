namespace ZIT.ThirdPartINTFC.Model
{
    /// <summary>
    /// 工单同步
    /// </summary>
    public class JhWorkorder
    {
        private string _zldbh;

        private string _bjr;

        private string _xb;

        private string _lxdh;

        private string _sfdz;

        private string _bjsj;

        private string _bjnr;

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
        /// 报警人
        /// </summary>
        public string Bjr { get => _bjr; set => _bjr = value; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Xb { get => _xb; set => _xb = value; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Lxdh { get => _lxdh; set => _lxdh = value; }

        /// <summary>
        /// 事发地址
        /// </summary>
        public string Sfdz { get => _sfdz; set => _sfdz = value; }

        /// <summary>
        /// 报警时间
        /// </summary>
        public string Bjsj { get => _bjsj; set => _bjsj = value; }
        
        /// <summary>
        /// 报警内容
        /// </summary>
        public string Bjnr { get => _bjnr; set => _bjnr = value; }

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
