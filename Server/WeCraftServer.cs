using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using Riptide;
using WeCraft.Core;
using WeCraft.Core.Entity;
using WeCraft.Core.Network;
using WeCraft.Core.World;
using WeCraftServer.Configuration;
using WeCraftServer.Mod;
using WeCraftServer.Network;
using WeCraftServer.Utility;

namespace WeCraftServer
{
    public class WeCraftServer: WeCraftCore
    {
        protected static WeCraftServer ServerInstance;
        //todo: 这些以后可以用DI, 来让插件获取其他插件之类的.
        public IConfiguration Configuration { get; protected set; }
        public CancellationTokenSource CancelToken { get; set; }
        public NetworkManager NetworkManager { get; protected set; }

        public readonly int MsPerTick;
 
        public Dictionary<Connection, Player> OnlinePlayers { get; private set; }

        public HashSet<World> Worlds { get; private set; }

        protected WeCraftServer()
        {
            
            CoreImpl = this;
            ServerInstance = this;
            
            //init value
            this.Configuration = new Configuration.Configuration();
            this.LoggerImpl = LogManager.GetLogger("WeCraftServer"); 
            this.CancelToken = new CancellationTokenSource();
            this.MsPerTick = 1000 / this.Configuration.TickRate;
            this.IsServer = true;
            this.OnlinePlayers = new Dictionary<Connection, Player>(Configuration.MaxPlayers,new ConnectionComparer());
            //set up manager
            this.GameLogicImpl = new global::WeCraftServer.Game.GameLogic(this);
            this.NetworkManager = new NetworkManager(this);
            this.NetworkManagerImpl = NetworkManager;
            this.ModManagerImpl = new ModManager(this);
            this.NetworkHandlerImpl = new NetworkHandler();
            this.CancelToken = new CancellationTokenSource();
            
            
            (this.ModManagerImpl as ModManager).Load();
            Worlds = new HashSet<World>()
            {
                new World(this.Configuration.Worlds[0].Name)
            };
        }

        public static WeCraftServer GetInstance()
        {
            if (CoreImpl == null)
            {
                var weCraftServer = new WeCraftServer();
            }

            return ServerInstance;
        }
        
        public void Run()
        {
            LoggerImpl.Info($"Server is running on {Configuration.Ip}:{Configuration.Port}");
            LoggerImpl.Info($"Tick-Rate {Configuration.TickRate}");
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

        public bool GetPlayer(Connection con,out Player player)
        {
            return OnlinePlayers.TryGetValue(con, out player);
        }

        public bool GetPlayer(string name, out Player player)
        {
            player = default;
            foreach (var onlinePlayersValue in OnlinePlayers.Values)
            {
                if (onlinePlayersValue.Name == name)
                {
                    player = onlinePlayersValue;
                    return true;
                }
            } 
            return false;
        }
        public bool GetPlayer(Guid name, out Player player)
        {
            player = default;
            foreach (var onlinePlayersValue in OnlinePlayers.Values)
            {
                if (onlinePlayersValue.Guid == name)
                {
                    player = onlinePlayersValue;
                    return true;
                }
            } 
            return false;
        }

        public bool GetWorld(string name,out World world)
        {
            world = default;
            foreach (var world1 in Worlds)
            {
                if (world1.Name.Equals(name))
                {
                    world = world1;
                    return true;
                }
            }
            return false;
        }
    }
}