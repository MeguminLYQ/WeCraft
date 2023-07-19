using ProtoBuf;
using WeCraft.Core.Common;

namespace WeCraft.Core.C2S
{
    [ProtoContract]
    public class C2S_PlayerMovement
    {
        [ProtoMember(1)]
        public int Id;
        [ProtoMember(2)]
        public Vector3 Position;
        [ProtoMember(3)] 
        public float Yaw;
    }
}