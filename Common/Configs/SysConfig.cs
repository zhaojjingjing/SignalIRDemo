using System.Configuration;

namespace Common.Configs
{
    public static class SysConfig
    {
        private static string _signalIRChatService;

        public static string SignalIRChatService
        {
            get { return _signalIRChatService;}
            set { _signalIRChatService = value; }

        }

        private static string _userName;

        public static string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        static SysConfig()
        {
            _signalIRChatService = ConfigurationManager.AppSettings["SignalIRChatService"];
            _userName = ConfigurationManager.AppSettings["UserName"];
        }
    }
}
