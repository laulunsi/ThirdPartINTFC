namespace ZIT.ThirdPartINTFC.Model
{
    /// <summary>
    /// 退单结果反馈
    /// </summary>
    public class JhChargebackresult
    {
        private string _zldbh;

        private string _tdbh;

        private string _tdjg;

        private string _jjtdly;

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
        /// 退单结果 
        /// 0:接受退单
        /// 1:拒绝退单
        /// </summary>
        public string Tdjg { get => _tdjg; set => _tdjg = value; }

        /// <summary>
        /// 拒绝退单理由
        /// </summary>
        public string Jjtdly { get => _jjtdly; set => _jjtdly = value; }
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
