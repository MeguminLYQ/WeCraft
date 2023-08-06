using WeCraft.Core.Mod;

namespace WeCraft.Core.Event
{
    public class EventHandlerBase
    {
        public ModId ModId { get; protected set; }
        public byte Priority { get; protected set; }
    }

    public class EventHandler<T>:EventHandlerBase where T : EventBase
    {
        public ExecuteDelegate<T> Delegate { get; }
        
        public EventHandler(ModId modId,ExecuteDelegate<T> @delegate,byte priority)
        {
            this.ModId = modId;
            this.Delegate = @delegate;
            this.Priority = priority;
        }

        public void Execute(T t)
        {
            Delegate(t);
        }
    }
}