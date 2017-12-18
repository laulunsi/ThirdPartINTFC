namespace ZIT.ThirdPartINTFC.Model
{
    public class ResultInfo
    {
        private int _result;

        private MsgType _type;

        private string _reason;

        public int Result { get => _result; set => _result = value; }
        public MsgType Type { get => _type; set => _type = value; }
        public string Reason { get => _reason; set => _reason = value; }
    }

    public enum MsgType
    {
        JhSigninfo = 0,
        JhChargeback = 1,
        JhFeedback = 2,
        JhAmbulanceinfo = 3
    }
}