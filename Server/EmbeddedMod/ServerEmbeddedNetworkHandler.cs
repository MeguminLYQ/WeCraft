using NLog;
using Riptide;
using WeCraft.Core.C2S;
using WeCraft.Core.Entity;
using WeCraft.Core.Utility;

namespace WeCraftServer.EmbeddedMod
{
    public class ServerEmbeddedNetworkHandler
    {
        private WeCraftServer server;
        private ServerEmbeddedMod mod;
        public ServerEmbeddedNetworkHandler(ServerEmbeddedMod mod)
        {
            this.mod = mod;
            server=WeCraftServer.GetInstance();
        }
        public void OnPlayerJoin(ushort clientId, byte[] data)
        {
            var bean=ProtobufUtility.Deserialize<C2S_PlayerProfile>(data);
            mod.Logger.Info($"玩家{bean.Name}:{bean.Guid}加入游戏");
            if (server.GetPlayer(bean.Name, out Player player))
            {
                mod.Logger.Warn($"玩家{bean.Name}已存在,但是还是发送消息包");
                return;
            }

            //todo: 从文件中读取位置,和坐标和世界,如果世界不存在,则返回初始世界
            var world=server.Worlds.GetEnumerator().Current;
            world.AddPlayer(bean,out player);
            server.NetworkManager.EmbeddedServer.TryGetClient(clientId, out Connection connection);
            server.OnlinePlayers.Add(connection,player);
        }
    }
}