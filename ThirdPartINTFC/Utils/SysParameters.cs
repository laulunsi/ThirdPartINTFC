using System.Configuration;

namespace ZIT.ThirdPartINTFC.Utils
{
    public class SysParameters
    {
        static SysParameters()
        {
            SharkHandsInterval = 10;

            SoftName = ConfigurationManager.AppSettings["SoftName"];


            DbConnectStr = ConfigurationManager.AppSettings["DBConnectStr"];

            BssServerIp = ConfigurationManager.AppSettings["BSSServerIP"];
            BssServerPort = short.Parse(ConfigurationManager.AppSettings["BSSServerPort"]);
            BssLocalPort = short.Parse(ConfigurationManager.AppSettings["BSSLocalPort"]);
            GpsLocalPort = short.Parse(ConfigurationManager.AppSettings["GPSLocalPort"]);

            QueryInterval = short.Parse(ConfigurationManager.AppSettings["QueryInterval"]);
            LocalUnitCode = ConfigurationManager.AppSettings["LocalUnitCode"];
        }
        /// <summary>
        /// 软件的名称
        /// </summary>
        public static string SoftName;
        /// <summary>
        /// 与各服务器握手时间间隔，单位：秒
        /// </summary>
        public static int QueryInterval;// = 10
        /// <summary>
        /// 120业务服务器IP地址
        /// </summary>
        public static string BssServerIp;// = "192.168.0.254";
        /// <summary>
        /// 与120业务服务器连接的本地端口
        /// </summary>
        public static short BssLocalPort;// = 2000;

        /// <summary>
        /// 120业务服务器监听端口
        /// </summary>
        public static short BssServerPort;// = 1003;
        /// <summary>
        /// 本地单位编号
        /// </summary>
        public static string LocalUnitCode;// = "000000";
        /// <summary>
        /// 与各服务器握手时间间隔，单位：秒
        /// </summary>
        public static int SharkHandsInterval;// = 5

        /// <summary>
        /// Gps本地端口
        /// </summary>
        public static short GpsLocalPort { get; private set; }
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string DbConnectStr { get; private set; }
    }
}
