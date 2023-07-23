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
            NetworkManager = WeCraftCore.CoreImpl.NetworkManagerImpl;
        }


        public override void OnLoad()
        {
        }

        public override void OnEnable()
        {
            Handler = new RootNetworkHandler(this);
            if (WeCraftCore.CoreImpl.IsClient)
            {
                // DefaultChannel.RegisterHandler((ushort)PackId.C2S_Ping,Handler.HandleC2SPing);
                // DefaultChannel.RegisterHandler((ushort)PackId.C2S_PlayerProfile,Handler.HandleC2SPlayerProfile);
            }
            if (WeCraftCore.CoreImpl.IsServer)
            {  
            }
        }

        public override void OnDisable()
        {
            if (WeCraftCore.CoreImpl.IsClient)
            {
                // DefaultChannel.RemoveHandler((ushort)PackId.C2S_Ping,Handler.HandleC2SPing);
                // DefaultChannel.RegisterHandler((ushort)PackId.C2S_PlayerProfile,Handler.HandleC2SPlayerProfile);
            }
            
        }
 
    }
}