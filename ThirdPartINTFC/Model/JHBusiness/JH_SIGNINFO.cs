namespace ZIT.ThirdPartINTFC.Model
{
    /// <summary>
    /// 工单签收
    /// </summary>
    public class JhSigninfo
    {
        private string _zldbh;

        private string _qsdw;

        private string _qsr;

        private string _qssj;

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
        /// 签收单位
        /// </summary>
        public string Qsdw { get => _qsdw; set => _qsdw = value; }

        /// <summary>
        /// 签收人名
        /// </summary>
        public string Qsr { get => _qsr; set => _qsr = value; }

        /// <summary>
        /// 签收时间
        /// </summary>
        public string Qssj { get => _qssj; set => _qssj = value; }

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
