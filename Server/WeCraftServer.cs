using System;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using WeCraft.Core;
using WeCraft.Core.Network;
using WeCraftServer.Configuration;
using WeCraftServer.Mod;
using WeCraftServer.Network;

namespace WeCraftServer
{
    public class WeCraftServer: WeCraftCore
    {
        protected static WeCraftServer ServerInstance;
        //todo: 这些以后可以用DI, 来让插件获取其他插件之类的.
        public ISetting Setting { get; protected set; }
        public CancellationTokenSource CancelToken { get; set; }

        public readonly int MsPerTick; 

        protected WeCraftServer()
        {
            //init value
            this.Setting = new Setting();
            this.LoggerImpl = LogManager.GetLogger("WeCraftServer"); 
            this.CancelToken = new CancellationTokenSource();
            this.MsPerTick = 1000 / this.Setting.TickRate;
            this.IsServer = true;
            //set up manager
            this.GameLogicImpl = new global::WeCraftServer.Game.GameLogic(this);
            this.NetworkManagerImpl = new NetworkManager(this);
            this.ModManagerImpl = new ModManager(this);
            this.NetworkHandlerImpl = new NetworkHandler();
            this.CancelToken = new CancellationTokenSource();

            (this.ModManagerImpl as ModManager).Load();

        }

        public static WeCraftServer GetInstance()
        {
            if (Instance == null)
            {
                var weCraftServer = new WeCraftServer();
                Instance = weCraftServer;
                ServerInstance = weCraftServer;
            }

            return ServerInstance;
        }
        
        public void Run()
        {
            LoggerImpl.Info($"Server is running on {Setting.Ip}:{Setting.Port}");
            LoggerImpl.Info($"Tick-Rate {Setting.TickRate}");
            LoggerImpl.Info($"Ms-Per-Tick {MsPerTick}");
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
                    GameLogicImpl.Tick();
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