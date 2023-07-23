using System;
using ProtoBuf;

namespace WeCraft.Core.Math
{
    [Serializable]
    [ProtoContract]
    public struct Vector3Short
    {
        [ProtoMember(1)] public short X;
        [ProtoMember(2)] public short Y;
        [ProtoMember(3)] public short Z;
    }
}