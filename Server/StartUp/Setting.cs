namespace WeCraftServer
{
    public class Setting:ISetting
    {
        public string Ip { get; } = "127.0.0.1";
        public int Port { get; } = 25575;
        public int TickRate { get; } = 20;
    }
}