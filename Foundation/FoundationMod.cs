using WeCraft.Core;
using WeCraft.Core.Mod;
using WeCraft.Core.Network; 
namespace WeCraft.Foundation
{
    public class FoundationMod: ModBase
    {
        public WeCraft.Core.Network.Channel DefaultChannel = NetworkHandler.DefaultChannel;
        public RootNetworkHandler Handler;
        public INetworkManager NetworkManager { get; set; }
        public FoundationMod()
        {
            Name = "Foundation";
            NetworkManager = WeCraftCore.Instance.NetworkManagerImpl;
        }


        public override void OnLoad()
        {
        }

        public override void OnEnable()
        {
            Handler = new RootNetworkHandler(this);
            DefaultChannel.RegisterHandler((ushort)PackId.C2S_Ping,Handler.HandlePing);  
        }

        public override void OnDisable()
        {
            DefaultChannel.RemoveHandler((ushort)PackId.C2S_Ping,Handler.HandlePing);
        }
    }
}