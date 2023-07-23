using WeCraft.Core.Entity;

namespace WeCraft.Core.EventHandler
{
    public class PlayerJoinEvent:Event,ICancellable
    {
        public Player Player;
        public bool Cancelled { get; set; }
        public void SetCancelled(bool can)
        {
            Cancelled = can;
        }
    }
}