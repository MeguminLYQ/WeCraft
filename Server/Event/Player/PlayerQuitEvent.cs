using WeCraft.Core.Event;

namespace WeCraftServer.Event.Player
{
    public class PlayerQuitEvent: EventBase
    {
        public WeCraft.Core.Entity.Player Player { get; }
        public string Message;

        public PlayerQuitEvent(WeCraft.Core.Entity.Player player)
        { 
            this.Player = player;
        }
    }
}