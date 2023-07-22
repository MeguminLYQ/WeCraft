using System; 
using System.Threading;
using System.Threading.Tasks;
using WeCraft.Core;
using NLog;
using WeCraft.Core.Game;
using WeCraft.Core.Network;
using WeCraft.Core.Plugin;

namespace WeCraft.Core
{
    public class Server: IServer
    {
        //todo: 这些以后可以用DI, 来让插件获取其他插件之类的.
        public ISetting Setting { get; protected set; }
        public ILogger Logger { get; protected set; }
        public WeCraft.Core.WeCraftCore Core { get; protected set; }
        public IGameLogic GameLogic { get; protected set; }
        public INetworkManager NetworkManager { get; protected set; }
        
        public IPluginManager PluginManager { get; protected set; }
        
        public readonly int MsPerTick;
        public CancellationTokenSource CancelToken { get; protected set; }

        public Server(ILogger logger, ISetting setting,WeCraft.Core.WeCraftCore core)
        {
            //init value
            this.Setting = setting;
            this.Logger = logger;
            this.Core = core;
            this.CancelToken = new CancellationTokenSource();
            this.MsPerTick = 1000 / setting.TickRate;
            
            //set up manager
            this.GameLogic = new GameLogic(this);
            this.NetworkManager = new NetworkManager(this);
            this.PluginManager = new PluginManager(this);
            

        }
        public void Run()
        {
            Logger.Info($"Server is running on {Setting.Ip}:{Setting.Port}");
            Logger.Info($"Tick-Rate {Setting.TickRate}");
            Logger.Info($"Ms-Per-Tick {MsPerTick}");
            Tick();
        }

        public void Stop()
        {
            CancelToken.Cancel();
        }

        public void Restart()
        {
            throw new System.NotImplementedException();
        }
        public async void Tick()
        {
            DateTime nextLoop = DateTime.Now;
            while (!CancelToken.IsCancellationRequested)
            {
                while (nextLoop<DateTime.Now)
                { 
                    GameLogic.Tick();
                    nextLoop = nextLoop.AddMilliseconds(MsPerTick);
                    if (nextLoop > DateTime.Now)
                    {
                        await Task.Delay(nextLoop - DateTime.Now);
                    }
                }
            }
        }
    }
}