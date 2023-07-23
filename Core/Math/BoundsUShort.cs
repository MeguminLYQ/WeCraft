using System;
using System.Numerics;
using ProtoBuf;

namespace WeCraft.Core.Math
{
    [Serializable]
    [ProtoContract]
    public struct BoundsUShort
    {
        [ProtoMember(1)]
        public Vector3UShort PointA;
        [ProtoMember(2)]
        public Vector3UShort PointB;
    }
}