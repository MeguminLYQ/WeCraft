using System;
using System.Collections.Generic;
using System.Net.Sockets.Kcp.Simple;
using System.Threading.Tasks;
using Core;
using Core.Util;
using NLog;

namespace WeCraftServer.Network
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
            Connection.kcp.Update(DateTimeOffset.UtcNow);
            await Task.Delay(50,this._server.CancelToken.Token);
        }

        
        /// <summary>
        /// 接收,非线程安全
        /// </summary>

        protected async void ListenNetwork()
        {
            while (!this._server.CancelToken.IsCancellationRequested)
            {
                byte[] res=await Connection.ReceiveAsync();
                int readPos = 0;
                uint chanId = BitConverter.ToUInt32(res, readPos);
                readPos += 4;
                uint cmd=BitConverter.ToUInt32(res, readPos);
                readPos += 4;
                ArraySegment<byte> segment = new ArraySegment<byte>(res, readPos, res.Length - readPos);
                if (!Handler.GetChannel(chanId, out Channel channel))
                {
                    this.Logger.Error($"网络找不到频道{chanId}");
                }

                if (!channel.HandlePacket(cmd, segment.Array))
                {
                    this.Logger.Error($"频道{chanId}找不到对应{cmd}的处理器");
                }
            }
        }
        
        /// <summary>
        /// 发送,线程安全
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        public async void Send(uint chanId,uint id,Object data)
        {
            Packet packet = new Packet();
            packet.Cmd = id;
            packet.Channel = chanId;
            packet.Data = PBUtil.Serialize(data); 
            
            var cmd=BitConverter.GetBytes(packet.Cmd);
            var channel=BitConverter.GetBytes(packet.Channel);
            int len = cmd.Length + channel.Length + packet.Data.Length;

            List<byte> buffer = new List<byte>(len);
            
            buffer.AddRange(channel);
            buffer.AddRange(cmd);
            buffer.AddRange(packet.Data); 
            Connection.SendAsync(buffer.ToArray(),len);
        }

        public async void Send(uint chanId, PackId id, object data)
        {
            Send(chanId,(uint)id,data);
        }
    }
}