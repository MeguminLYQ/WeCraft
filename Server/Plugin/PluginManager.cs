namespace WeCraft.Core.Plugin
{
    public class PluginManager: IPluginManager
    {
        private IServer Server;
        public PluginManager(IServer server)
        {
            this.Server = server;
            Plugins = LoadAllPlugin();
            Server.Logger.Info("开始加载插件");
            foreach (var pluginBase in Plugins)
            {
                Server.Logger.Info($"开始加载{pluginBase.Name}");
                pluginBase.OnEnable();
            }
            
            Server.Logger.Info("插件加载完成");
        }

        public PluginBase[] Plugins { get; protected set; }
        protected PluginBase[] LoadAllPlugin()
        {
            return new PluginBase[] { new RootPlugin() };
        }
    }
}