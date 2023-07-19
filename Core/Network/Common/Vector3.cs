using ProtoBuf;

namespace WeCraft.Core.Common
{
    [ProtoContract]
    public class Vector3
    {
        [ProtoMember(1)] public float X;
        [ProtoMember(2)] public float Y;
        [ProtoMember(3)] public float Z;
    }
}