namespace WeCraft.Core.Event
{
    public class EventBase
    {
        public EventPriority Priority = EventPriority.Normal;
        public bool Cancelled { get; set; }
        public bool Cancellable { get ; protected set; }
        
        public bool IsCancelled=>!(Cancellable && Cancelled);
    }
}