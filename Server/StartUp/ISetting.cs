﻿namespace WeCraftServer
{
    public interface ISetting
    {
        public string Ip { get; }
        public int Port {get;}
        public int TickRate { get; }
    }
} 