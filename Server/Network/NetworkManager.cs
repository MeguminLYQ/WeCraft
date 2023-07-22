 
using System.Threading.Tasks;
using WeCraft.Core;
using WeCraft.Core.Util;
using NLog;
using Riptide;
using Riptide.Utils;

namespace WeCraft.Core.Network
{
    public class NetworkManager:INetworkManager
    { 
        public Riptide.Server EmbeddedServer { get; protected set; }
        public NetworkHandler Handler { get; protected set; }
        public Logger Logger;
        private IServer _server;
        public NetworkManager(IServer server)
        {
            this._server = server;
            this.Handler = server.Core.Handler;
            this.Logger = LogManager.GetCurrentClassLogger();
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
            EmbeddedServer.MessageReceived += OnMessageReceived;
            EmbeddedServer.Start(this._server.Setting.Port,10,0,false);
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
            Handler.HandleMessage(clientId,message.GetUShort(),message.GetUShort(),message.GetBytes());
        }


        /// <summary>
        /// 发送,线程安全
        /// </summary>
        /// <param name="packId"></param>
        /// <param name="data"></param>
        public void Send(ushort[] clientId, ushort chanId, ushort packId, object data, bool reliable = true)
        {
            var message = CreateMessage(chanId, packId, data, reliable);
            for (var i = 0; i < clientId.Length; i++)
            {
                EmbeddedServer.Send(message,clientId[i],false);
            }
            message.Release();
        }

        public void Send(ushort[] clientID, ushort chanId, PackId id, object data, bool reliable = true)
        {
            Send(clientID, chanId, (ushort)id, data,reliable);
        }
 

        public Message CreateMessage(ushort chanId, ushort packId, object data, bool reliable = true)
        {
            Message message = reliable
                ? Message.Create(MessageSendMode.Reliable,0)
                : Message.Create(MessageSendMode.Unreliable,0);
            message.AddUShort(chanId);
            message.AddUShort(packId);
            message.AddBytes(PBUtil.Serialize(data));
            return message;
        }
        
    }
}