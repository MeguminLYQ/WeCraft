using WeCraft.Core.Event;

namespace WeCraftServer.Event.Player
{
    public class PlayerKickEvent:EventBase
    {
        public WeCraft.Core.Entity.Player Player { get; }
        public string Message;

        public PlayerKickEvent(WeCraft.Core.Entity.Player player)
        {
            Cancellable = true;
            this.Player = player;
        }
    }
}