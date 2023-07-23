 
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
        public void HandlePing(ushort clientId,byte[] data)
        {
            var ping=ProtobufUtility.Deserialize<C2S_Ping>(data);
            S2C_Pong pong = new S2C_Pong()
            {
                msg = ping.msg + "|pong|"+clientId
            };
            Mod.NetworkManager.Send(new []{clientId},Mod.DefaultChannel.Id,PackId.S2C_Pong,pong);
        }
    }
}