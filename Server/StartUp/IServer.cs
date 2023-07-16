using System.Threading;
using Core;
using NLog;
using WeCraftServer.Game;
using WeCraftServer.Network;

namespace WeCraftServer
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