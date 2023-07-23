namespace WeCraft.Core.Network
{
    public struct Packet
    {
        public uint Cmd;
        public uint Channel;
        public byte[] Data;
    }
}