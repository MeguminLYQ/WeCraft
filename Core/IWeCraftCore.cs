using System.Threading;
using NLog;
using WeCraft.Core;
using WeCraft.Core.Event;
using WeCraft.Core.EventHandler;
using WeCraft.Core.GameLogic;
using WeCraft.Core.Mod;
using WeCraft.Core.Network;

namespace WeCraft.Core
{
    public interface IWeCraftCore
    {
        public IModManager ModManagerImpl { get; }
        public INetworkManager NetworkManagerImpl { get; }
        public ILogger LoggerImpl { get; }
        public NetworkHandler NetworkHandlerImpl { get; }
        public EventBus EventBus { get; }
        public bool IsClient { get; }
        public bool IsHost { get; }
        public bool IsServer { get; }  
        
        public IGameLogic GameLogicImpl { get; } 
    }
}
 