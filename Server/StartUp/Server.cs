using System;
using System.Net.Sockets.Kcp.Simple;
using System.Threading;
using System.Threading.Tasks;
using Core;
using NLog;
using WeCraftServer.Game;
using WeCraftServer.Network;

namespace WeCraftServer
{
    public class Server: IServer
    {
        public ISetting Setting { get; protected set; }
        public ILogger Logger { get; protected set; }
        public WeCraftCore Core { get; protected set; }
        public IGameLogic GameLogic { get; protected set; }
        public INetworkManager NetworkManager { get; protected set; }
        public readonly int MsPerTick;
        public CancellationTokenSource CancelToken { get; protected set; }

        public Server(ILogger logger, ISetting setting,WeCraftCore core)
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