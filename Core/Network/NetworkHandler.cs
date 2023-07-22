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
        public delegate void Handle(ushort clientId,byte[] data);

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

        
        public bool RegisterChannel(string name,ushort id, out Channel channel)
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
            ushort maxValue = UInt16.MinValue;
            foreach (var chan in Channels)
            {
                if (chan.Name.Equals(name))
                    return false;
                if (chan.Id > maxValue)
                {
                    maxValue = chan.Id;
                }
            }

            try
            {
                maxValue += 1;
            }
            catch (System.Exception e)
            {
                WeCraftCore.Logger.Error("自动注册失败, 似乎所有id已经注册满了?");
            }
            
            channel = new Channel(name,maxValue);
            Channels.Add(channel);
            return true;
        }

        public byte[] GetSendBytes(ushort chanId, ushort id, object data)
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

        public byte[] GetSendBytes(ushort chanId, PackId id, object data)
        {
            return GetSendBytes(chanId,(ushort) id, data);
        }

        public void HandleRawMessage(ushort clientId,byte[] res)
        {
            //长度不足
            if(res.Length<4*3)
                return;
            int readPos = 0;
            int len = BitConverter.ToInt32(res, readPos);
            readPos += sizeof(int);
            ushort chanId = BitConverter.ToUInt16(res, readPos);
            readPos += sizeof(ushort);
            ushort cmd=BitConverter.ToUInt16(res, readPos);
            readPos += sizeof(ushort);
            // ArraySegment<byte> segment = new ArraySegment<byte>(res, readPos, res.Length - readPos);
            // res.Skip(readPos).ToArray()
            byte[] data = new byte[res.Length-readPos];
            Array.Copy(res,readPos,data,0,data.Length);
            HandleMessage(clientId,chanId,cmd,data);
        }
        
        public void HandleMessage(ushort clientId,ushort chanId, ushort packId, byte[] data)
        {
            if (!GetChannel(chanId, out Channel channel))
            {
                WeCraftCore.Logger.Error($"网络找不到频道{chanId}");
                return;
            }
            if (!channel.HandlePacket(clientId,packId, data))
            {
                WeCraftCore.Logger.Error($"频道{chanId}找不到对应{packId}的处理器");
                return;
            } 
            WeCraftCore.Logger.Debug($"处理{chanId}:{packId}包");
        }
        
    }
}