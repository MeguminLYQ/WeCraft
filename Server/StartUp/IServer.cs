using System.Threading;
using WeCraft.Core;
using NLog;
using WeCraft.Core.Game;
using WeCraft.Core.Network;

namespace WeCraft.Core
{
    public interface IServer
    {
        public ISetting Setting { get; }
        public INetworkManager NetworkManager { get; }
        public IGameLogic GameLogic { get; }
        public ILogger Logger { get; }
        
        public WeCraftCore Core { get; }
        
        public CancellationTokenSource CancelToken { get; }
        
        public void Run();
        public void Stop();
        public void Restart();
    }
}