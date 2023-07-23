 
using WeCraft.Core.Mod;
using WeCraft.Foundation;

namespace WeCraftServer.Mod
{
    public class ModManager: IModManager
    {
        private WeCraftServer Server;
        public ModBase[] Mods { get; protected set; }
        public void CallPreInitialization()
        {
            throw new System.NotImplementedException();
        }

        public void CallInitialization()
        {
            throw new System.NotImplementedException();
        }

        public void CallPostInitialization()
        {
            throw new System.NotImplementedException();
        }

        public ModManager(WeCraftServer server)
        { 
            this.Server = server;
        }

        public void Load()
        {
            Mods = LoadAllMod();
            Server.LoggerImpl.Info("开始加载插件");
            foreach (var pluginBase in Mods)
            {
                Server.LoggerImpl.Info($"开始加载{pluginBase.Name}");
                pluginBase.OnEnable();
            }
            
            Server.LoggerImpl.Info("插件加载完成");
        }
        protected ModBase[] LoadAllMod()
        {
            return new ModBase[] { new FoundationMod() };
        }
    }
}