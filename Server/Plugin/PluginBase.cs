using WeCraft.Core;
using WeCraft.Core.Network;

namespace WeCraft.Core.Plugin
{
    public abstract class PluginBase
    {
        public WeCraft.Core.WeCraftCore Core => WeCraftServer.Instance.Core;
        public IServer Server => WeCraftServer.Instance.Server;
        public INetworkManager NetworkManager => WeCraftServer.Instance.Server.NetworkManager;
        public string Name { get; protected set; }
        public abstract void OnLoad();
        public abstract void OnEnable();
        public abstract void OnDisable();
    }
}