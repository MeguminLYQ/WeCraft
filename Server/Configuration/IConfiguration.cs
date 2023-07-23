namespace WeCraftServer.Configuration
{
    public interface IConfiguration
    {
        public string Ip { get; }
        public ushort Port {get;}
        /// <summary>
        /// tick per second
        /// </summary>
        public byte TickRate { get; }

        public ushort MaxPlayers { get;}
        
        public string DefaultWorld { get; }
        public WorldSection[] Worlds { get; }
    }
} 