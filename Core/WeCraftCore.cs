using System.Threading;
using NLog;
using WeCraft.Core.GameLogic;
using WeCraft.Core.Mod;
using WeCraft.Core.Network;

namespace WeCraft.Core
{
    public abstract class WeCraftCore: IWeCraftCore
    {
        
        public static IWeCraftCore CoreImpl;
        public IModManager ModManagerImpl { get; protected set; }
        public INetworkManager NetworkManagerImpl { get; protected set; }
        public ILogger LoggerImpl { get; protected set; } 
        public IGameLogic GameLogicImpl { get; protected set; }
        public NetworkHandler NetworkHandlerImpl { get; protected set; }
        public EventHandler.EventHandler EventHandler { get; protected set; }
        public bool IsClient { get; protected set; }
        public bool IsHost { get; protected set; }
        public bool IsServer { get; protected set; }

        protected WeCraftCore()
        {
            if (CoreImpl != null)
            {
                LoggerImpl.Error("已经存在WeCraftCore实例");
            }
            CoreImpl = this;
        }
    }
}