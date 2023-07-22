namespace WeCraft.Core
{
    public interface ISetting
    {
        public string Ip { get; }
        public ushort Port {get;}
        /// <summary>
        /// tick per second
        /// </summary>
        public byte TickRate { get; }
    }
} 