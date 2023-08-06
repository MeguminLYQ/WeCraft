using WeCraft.Core.Event;

namespace WeCraftServer.Event.Player
{
    public class PlayerJoinEvent:EventBase
    {
        public WeCraft.Core.Entity.Player Player { get; }
        public string Message;

        public PlayerJoinEvent(WeCraft.Core.Entity.Player player)
        {
            Cancellable = true;
            this.Player = player;
        }
    }
}