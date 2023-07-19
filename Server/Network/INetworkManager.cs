using WeCraft.Core;

namespace WeCraft.Core.Network
{
    public interface INetworkManager
    {
        public void Send(uint chanId, uint id, object data);
        public void Send(uint chanId, PackId id, object data);
    }
}