using System;
using ProtoBuf;

namespace WeCraft.Core.Math
{
    [Serializable]
    [ProtoContract]
    public struct Vector2
    {
        [ProtoMember(1)] public float X;
        [ProtoMember(2)] public float Y;
    }
}