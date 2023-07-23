using WeCraft.Core;

namespace WeCraft.Core.Network
{
    public interface INetworkManager
    {
        public void SendToClient(ushort[] clientId,ushort chanId, ushort id, object data,bool reliable=true);
        public void SendToClient(ushort[] clientId,ushort chanId, PackId id, object data,bool reliable=true);
        
        public void SendToAllClient(ushort chanId, ushort id, object data,bool reliable=true);
        public void SendToAllClient(ushort chanId, PackId id, object data,bool reliable=true);
        public void SendToServer(ushort chanId, PackId id, object data, bool reliable = true);
        public void SendToServer(ushort chanId, ushort id, object data, bool reliable = true);
    }
}