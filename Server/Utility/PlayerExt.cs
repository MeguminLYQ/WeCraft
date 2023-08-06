using System;
using System.Linq;
using Riptide;
using WeCraft.Core.Entity;
using WeCraft.Core.Event;
using WeCraftServer.Event.Player;

namespace WeCraftServer.Utility
{
    public static class PlayerExt
    {
 
        private static WeCraftServer _Server=WeCraftServer.GetInstance();
        public static bool IsOnline(this Player player)
        {
            if (player==null)
            {
                throw new NullReferenceException();
            }
            return WeCraftServer.GetInstance().OnlinePlayers.ContainsValue(player);
        }
        
        public static void Kick(this Player player)
        {
            var conn=player.GetConnection();
            PlayerKickEvent @event=new PlayerKickEvent(player);
            EventBus.Post(@event);
            if (@event.IsCancelled)
            {
                return;
            }
            conn.Disconnect();
        }

        public static Connection GetConnection(this Player player)
        {
           var pair=WeCraftServer.GetInstance().OnlinePlayers.FirstOrDefault(pair => pair.Value == player);
           return pair.Key;
        }
    }
}