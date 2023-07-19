using System;
using System.Collections.Generic;

namespace WeCraft.Core
{
    public struct Packet
    {
        public uint Cmd;
        public uint Channel;
        public byte[] Data;
    }
}