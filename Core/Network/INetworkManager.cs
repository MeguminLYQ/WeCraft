using WeCraft.Core;

namespace WeCraft.Core.Network
{
    public interface INetworkManager
    {
        public void Send(ushort[] clientId,ushort chanId, ushort id, object data,bool reliable=true);
        public void Send(ushort[] clientId,ushort chanId, PackId id, object data,bool reliable=true); 
    }
}