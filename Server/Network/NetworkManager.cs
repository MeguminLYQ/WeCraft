using System;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using Riptide;
using Riptide.Utils;
using WeCraft.Core.Entity;
using WeCraft.Core.Event;
using WeCraft.Core.EventHandler;
using WeCraft.Core.Network;
using WeCraft.Core.Utility;
using WeCraftServer.Event.Player;

namespace WeCraftServer.Network
{
    public class NetworkManager:INetworkManager
    { 
        public Riptide.Server EmbeddedServer { get; protected set; }
        public NetworkHandler Handler => _server.NetworkHandlerImpl;
        public Logger Logger;
        private WeCraftServer _server;
        public NetworkManager(WeCraftServer server)
        {
            this._server = server;
            this.Logger = LogManager.GetLogger("WeCraftServer.Network");
            RiptideLogger.Initialize(Logger.Debug,Logger.Info,Logger.Warn,Logger.Error,false);
            Task.Run(StartNetwork); 
        }

        protected async void StartNetwork()
        {
            EmbeddedServer = new Riptide.Server();
            EmbeddedServer.ClientConnected += (s, e) =>
            {
                e.Client.CanTimeout = false;
            };
            EmbeddedServer.ClientDisconnected += (s, e) =>
            {
                bool success = _server.GetPlayer(e.Client,out Player player);
                if (!success)
                {
                    Logger.Warn("断开了一个没有玩家的连接");
                    return;
                }

                switch (e.Reason)
                {
                    case DisconnectReason.Disconnected:
                        PlayerQuitEvent playerQuitEvent = new PlayerQuitEvent(player);
                        EventBus.Post(playerQuitEvent);
                        break;
                }
            };
            EmbeddedServer.MessageReceived += OnMessageReceived;
            EmbeddedServer.Start(this._server.Configuration.Port,this._server.Configuration.MaxPlayers,0,false);
            while (!this._server.CancelToken.IsCancellationRequested)
            {
                EmbeddedServer.Update();
                await Task.Delay(10,this._server.CancelToken.Token);
            }
            EmbeddedServer.Stop();
            EmbeddedServer.MessageReceived-=OnMessageReceived; 
            
        }

        private void OnMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            var message=e.Message;
            var clientId = e.FromConnection.Id;
            try
            {
                ushort chanId = message.GetUShort();
                ushort packId = message.GetUShort();
                byte[] data = message.GetBytes();
                Handler.HandleMessage(clientId,chanId,packId,data);
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
            }
        }


        /// <summary>
        /// 发送,线程安全
        /// </summary>
        /// <param name="packId"></param>
        /// <param name="data"></param>
        public void SendToClient(ushort[] clientId, ushort chanId, ushort packId, object data, bool reliable = true)
        {
            var message = CreateMessage(chanId, packId, data, reliable);
            for (var i = 0; i < clientId.Length; i++)
            {
                EmbeddedServer.Send(message,clientId[i],false);
            }
            message.Release();
        }

        public void SendToClient(ushort[] clientID, ushort chanId, PackId id, object data, bool reliable = true)
        {
            SendToClient(clientID, chanId, (ushort)id, data,reliable);
        }

        public void SendToClient(Connection[] clients, ushort chanId, ushort packId, object data, bool reliable = true)
        {
            var message = CreateMessage(chanId, packId, data, reliable);
            for (var i = 0; i < clients.Length; i++)
            {
                EmbeddedServer.Send(message,clients[i].Id,false);
            }
            message.Release();
        }
        public void SendToAllClient(ushort chanId, ushort id, object data, bool reliable = true)
        {
            SendToClient(EmbeddedServer.Clients,chanId,id,data,reliable);
        }

        public void SendToAllClient(ushort chanId, PackId id, object data, bool reliable = true)
        {
            SendToAllClient(chanId,(ushort)id,data,reliable);
        }


        public Message CreateMessage(ushort chanId, ushort packId, object data, bool reliable = true)
        {
            Message message = reliable
                ? Message.Create(MessageSendMode.Reliable,0)
                : Message.Create(MessageSendMode.Unreliable,0);
            message.AddUShort(chanId);
            message.AddUShort(packId);
            message.AddBytes(ProtobufUtility.Serialize(data));
            return message;
        }
 

        public void SendToServer(ushort chanId, PackId id, object data, bool reliable = true)
        {
            SendToServer(chanId, (ushort)id, data, reliable);
        }

        public void SendToServer(ushort chanId, ushort id, object data, bool reliable = true)
        {
            Logger.Warn("作为服务器端,尝试发送数据给服务器端");
        }
    }
}