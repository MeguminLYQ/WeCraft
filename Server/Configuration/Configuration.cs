namespace WeCraftServer.Configuration
{
    public class Configuration:IConfiguration
    {
        public string Ip { get; } = "127.0.0.1";
        public ushort Port { get; } = 25575;
        public byte TickRate { get; } = 20;
        public ushort MaxPlayers { get; } = 10;
        public string DefaultWorld { get; } = "World_Overworld";
        public WorldSection[] Worlds { get; } = new WorldSection[]
        {
            new WorldSection()
            {
                Generator = "foundation:overworld",
                Name = "World_Overworld",
                Path = "./World_Overworld",
                Seed = 1434967947,
            }
        };
    }
}