namespace ZIT.ThirdPartINTFC.Model
{
    public enum ConStatus
    {
        Connected,
        Login,
        DisConnected
    }

    public enum FunModule
    {
        Bs,
        Gs,
        Db
    }

    public class StatusChanged
    {
        private ConStatus _status;

        private FunModule _module;

        public ConStatus Status { get => _status; set => _status = value; }
        public FunModule Module { get => _module; set => _module = value; }
    }
}