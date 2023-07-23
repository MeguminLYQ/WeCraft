namespace WeCraft.Core.EventHandler
{
    public interface ICancellable
    {
        public bool Cancelled { get; }
        public void SetCancelled(bool can);
    }
}