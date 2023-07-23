using System.Threading.Channels;
using WeCraft.Core.GameLogic;

namespace WeCraftServer.Game
{
    public class GameLogic: IGameLogic
    {
        public Channel<string> CommandChannel;

        private WeCraftServer _server;
        public GameLogic(WeCraftServer server)
        {
            this._server = server;
        }

        public void Tick()
        {
        }
    }
}