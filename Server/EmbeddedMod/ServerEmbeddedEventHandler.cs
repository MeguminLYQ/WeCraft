using System;
using WeCraft.Core.Entity;
using WeCraft.Core.Event;
using WeCraft.Core.EventHandler;
using WeCraftServer.Event.Player;

namespace WeCraftServer.EmbeddedMod
{
    public class ServerEmbeddedEventHandler
    {
        private ServerEmbeddedMod mod;

        public ServerEmbeddedEventHandler(ServerEmbeddedMod mod)
        {
            this.mod = mod;
        }

        public void OnPlayerJoin(PlayerJoinEvent @event)
        {
            mod.Logger.Info($"{@event.Player.Name} 加入服务器");
        }
        
        public void OnPlayerQuit(PlayerQuitEvent @event){ 
            mod.Logger.Info($"{@event.Player.Name} 退出服务器");
        }
        
    }
}