using System;
using System.Collections.Generic;
using System.Net.Sockets.Kcp.Simple;
using System.Threading.Tasks;
using WeCraft.Core;
using WeCraft.Core.Util;
using NLog;

namespace WeCraft.Core.Network
{
    public class NetworkManager:INetworkManager
    { 
        public SimpleKcpClient Connection { get; protected set; }
        public NetworkHandler Handler { get; protected set; }
        public Logger Logger;
        private IServer _server;
        public NetworkManager(IServer server)
        {
            this._server = server;
            this.Handler = server.Core.Handler;
            this.Logger = LogManager.GetCurrentClassLogger();
            this.Connection = new SimpleKcpClient(this._server.Setting.Port);
            Task.Run(UpdateNetwork,this._server.CancelToken.Token);
            Task.Run(ListenNetwork,this._server.CancelToken.Token);
        }

        protected async void UpdateNetwork()
        {
            while (!this._server.CancelToken.IsCancellationRequested)
            {
                Connection.kcp.Update(DateTimeOffset.UtcNow);
                await Task.Delay(50,this._server.CancelToken.Token);
            }
        }

        
        /// <summary>
        /// 接收,非线程安全
        /// </summary>

        protected async void ListenNetwork()
        {
            while (!this._server.CancelToken.IsCancellationRequested)
            {
                byte[] res=await Connection.ReceiveAsync(); 
                Handler.HandleBytes(res);
            }
        }
        
        /// <summary>
        /// 发送,线程安全
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        public async void Send(uint chanId,uint id,object data)
        {
            var bytes = Handler.GetSendBytes(chanId, id, data);
            Connection.SendAsync(bytes,bytes.Length);
        }

        public async void Send(uint chanId, PackId id, object data)
        {
            Send(chanId,(uint)id,data);
        }
    }
}