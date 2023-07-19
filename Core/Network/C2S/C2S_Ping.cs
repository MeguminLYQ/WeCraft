using ProtoBuf;

namespace WeCraft.Core.C2S
{
    [ProtoContract]
    public class C2S_Ping
    {
        [ProtoMember(1)]
        public string msg = "ping";
    }
}