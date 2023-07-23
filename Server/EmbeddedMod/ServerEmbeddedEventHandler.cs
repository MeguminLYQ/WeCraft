using System;
using WeCraft.Core.Entity;

namespace WeCraftServer.EmbeddedMod
{
    public class ServerEmbeddedEventHandler
    {
        private ServerEmbeddedMod mod;

        public ServerEmbeddedEventHandler(ServerEmbeddedMod mod)
        {
            this.mod = mod;
        }

        public void OnPlayerQuit(Player player)
        {
            mod.Logger.Info($"{player.Name} 离开服务器");
            
        }
    }
}