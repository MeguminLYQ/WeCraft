using System.Threading.Channels;
using WeCraft.Core;
using Channel = WeCraft.Core.Network.Channel;

namespace WeCraft.Core.Plugin
{
    public class RootPlugin: PluginBase
    {
        public Network.Channel DefaultChannel = NetworkHandler.DefaultChannel;
        public RootNetworkHandler Handler;

        public RootPlugin()
        {
            Name = "Root";
        }
        public override void OnLoad()
        {
        }

        public override void OnEnable()
        {
            Handler = new RootNetworkHandler(this);
            DefaultChannel.RegisterHandler(PackId.C2S_Ping,Handler.HandlePing);  
        }

        public override void OnDisable()
        {
            DefaultChannel.RemoveHandler(PackId.C2S_Ping,Handler.HandlePing);
        }
    }
}