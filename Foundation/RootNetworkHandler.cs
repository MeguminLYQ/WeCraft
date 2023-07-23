 
using WeCraft.Core;
using WeCraft.Core.C2S;
using WeCraft.Core.Network;
using WeCraft.Core.S2C;
using WeCraft.Core.Utility;

namespace WeCraft.Foundation
{
    public class RootNetworkHandler
    {
        protected FoundationMod Mod;
        public RootNetworkHandler(FoundationMod rootMod)
        {
            this.Mod = rootMod;
        }
        public void HandleC2SPing(ushort clientId,byte[] data)
        {
            var ping=ProtobufUtility.Deserialize<C2S_Ping>(data);
            S2C_Pong pong = new S2C_Pong()
            {
                msg = ping.msg + "|pong|"+clientId
            };
            Mod.NetworkManager.SendToClient(new []{clientId},Mod.DefaultChannel.Id,PackId.S2C_Pong,pong);
        }

        public void HandleS2CPong(ushort clientId, byte[] data)
        {
            var pong = ProtobufUtility.Deserialize<S2C_Pong>(data);
            WeCraftCore.CoreImpl.LoggerImpl.Info(pong);
        }
        
        public void HandleC2SPlayerProfile(ushort clientId, byte[] data)
        {
            var profile = ProtobufUtility.Deserialize<C2S_PlayerProfile>(data);
        }
    }
}