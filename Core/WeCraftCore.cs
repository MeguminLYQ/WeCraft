using NLog;

namespace Core
{
    public class WeCraftCore
    {
        internal static ILogger Logger;
        public NetworkHandler Handler { get; private set; }
        public WeCraftCore()
        {
            Handler = new NetworkHandler();
            Logger = NLog.LogManager.GetCurrentClassLogger();
        }
 
    }
}