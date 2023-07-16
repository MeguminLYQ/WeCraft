using ProtoBuf;

namespace Core.S2C
{
    [ProtoContract]
    public class S2C_Pong
    {
        [ProtoMember(1)]
        public string msg = "pong";
    }
}