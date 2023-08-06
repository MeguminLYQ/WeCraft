namespace WeCraft.Core.Event
{
    public delegate void ExecuteDelegate<in T>(T @event) where T:EventBase;
}