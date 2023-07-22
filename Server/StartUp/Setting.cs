namespace WeCraft.Core
{
    public class Setting:ISetting
    {
        public string Ip { get; } = "127.0.0.1";
        public ushort Port { get; } = 25575;
        public byte TickRate { get; } = 20;
    }
}