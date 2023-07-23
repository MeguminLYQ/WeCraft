namespace WeCraftServer.Configuration
{
    public class WorldSection
    {
        public string Name;
        public string Registery = "default:world";
        public int Seed;
        /// <summary>
        /// 生成器,比如"foundation:flat"
        /// </summary>
        public string Generator="foundation:overworld";
        public string Path;
    }
}