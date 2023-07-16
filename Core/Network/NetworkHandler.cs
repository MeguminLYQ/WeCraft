using System;
using System.Collections.Generic;
using System.Linq;
using WeCraftServer.Network;

namespace Core
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
                if (chan.Name.Equals(name))
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
    }
}