using System;
using System.Collections.Generic;
using System.Linq;
using WeCraft.Core.Network;
using WeCraft.Core.Util;

namespace WeCraft.Core
{
    public class NetworkHandler
    {

        public static Channel DefaultChannel { get; protected set; } = new Channel("default", 0);
        protected static HashSet<Channel> Channels = new HashSet<Channel>(){DefaultChannel};
        public delegate void Handle(byte[] data);

        internal NetworkHandler()
        {
        }
        public bool GetChannel(uint id,out Channel channel)
        {
            channel = default;
            foreach (var chan in Channels)
            {
                if (chan.Id == id)
                {
                    channel = chan;
                    return true;
                }
            }
            return false;
        }

        public Channel GetDefaultChannel()
        {
            return DefaultChannel;
        }
        public bool GetChannel(string name, out Channel channel)
        {
            channel = default;
            foreach (var chan in Channels)
            {
                if (chan.Name.Equals(name))
                {
                    channel = chan;
                    return true;
                }
            }
            return false;
        }

        
        public bool RegisterChannel(string name,uint id, out Channel channel)
        {
            channel = default; 
            foreach (var chan in Channels)
            {
                if (chan.Name.Equals(name)||chan.Id==id)
                    return false; 
            } 
            channel = new Channel(name,id);
            Channels.Add(channel);
            return true;
        }
        
        public bool RegisterChannel(string name, out Channel channel)
        {
            channel = default;
            uint maxValue = UInt32.MinValue;
            foreach (var chan in Channels)
            {
                if (chan.Name.Equals(name))
                    return false;
                if (chan.Id > maxValue)
                {
                    maxValue = chan.Id;
                }
            } 
            channel = new Channel(name,maxValue+1);
            Channels.Add(channel);
            return true;
        }

        public byte[] GetSendBytes(uint chanId, uint id, object data)
        {
            Packet packet = new Packet();
            packet.Cmd = id;
            packet.Channel = chanId;
            packet.Data = PBUtil.Serialize(data); 
            
            var cmd=BitConverter.GetBytes(packet.Cmd);
            var channel=BitConverter.GetBytes(packet.Channel);
            int len = cmd.Length + channel.Length + packet.Data.Length;
            var lenBytes = BitConverter.GetBytes(len);
            List<byte> buffer = new List<byte>(len);
            buffer.AddRange(lenBytes);
            buffer.AddRange(channel);
            buffer.AddRange(cmd);
            buffer.AddRange(packet.Data);
            return buffer.ToArray();
        }

        public byte[] GetSendBytes(uint chanId, PackId id, object data)
        {
            return GetSendBytes(chanId,(uint) id, data);
        }

        public void HandleBytes(byte[] res)
        {
            //长度不足
            if(res.Length<4*3)
                return;
            int readPos = 0;
            int len = BitConverter.ToInt32(res, readPos);
            readPos += 4;
            uint chanId = BitConverter.ToUInt32(res, readPos);
            readPos += 4;
            uint cmd=BitConverter.ToUInt32(res, readPos);
            readPos += 4;
            // ArraySegment<byte> segment = new ArraySegment<byte>(res, readPos, res.Length - readPos);
            // res.Skip(readPos).ToArray()
            byte[] data = new byte[res.Length-readPos];
            Array.Copy(res,readPos,data,0,data.Length);
            if (!GetChannel(chanId, out Channel channel))
            {
                WeCraftCore.Logger.Error($"网络找不到频道{chanId}");
                return;
            }
            if (!channel.HandlePacket(cmd, data))
            {
                WeCraftCore.Logger.Error($"频道{chanId}找不到对应{cmd}的处理器");
                return;
            }
            
            WeCraftCore.Logger.Debug($"处理{chanId}:{cmd}包");
        }
        
        
    }
}