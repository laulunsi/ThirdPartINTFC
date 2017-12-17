namespace ZIT.ThirdPartINTFC.Model
{
    /// <summary>
    /// 工单处置回复
    /// </summary>
    public class JhFeedback
    {
        private string _zldbh;

        private string _fkdbh;

        private string _fkdw;

        private string _fkr;

        private string _fksj;

        private string _fknr;

        private string _fkjqlb;

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
        /// 反馈单编号
        /// </summary>
        public string Fkdbh { get => _fkdbh; set => _fkdbh = value; }

        /// <summary>
        /// 反馈单位
        /// </summary>
        public string Fkdw { get => _fkdw; set => _fkdw = value; }

        /// <summary>
        /// 反馈人
        /// </summary>
        public string Fkr { get => _fkr; set => _fkr = value; }

        /// <summary>
        /// 反馈时间
        /// </summary>
        public string Fksj { get => _fksj; set => _fksj = value; }

        /// <summary>
        /// 反馈内容
        /// </summary>
        public string Fknr { get => _fknr; set => _fknr = value; }

        /// <summary>
        /// 反馈警情类别(过程反馈/结果反馈)
        /// </summary>
        public string Fkjqlb { get => _fkjqlb; set => _fkjqlb = value; }

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