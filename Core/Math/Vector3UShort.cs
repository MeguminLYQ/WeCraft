using System;
using ProtoBuf;

namespace WeCraft.Core.Math
{
    [Serializable]
    [ProtoContract]
    public struct Vector3UShort
    {
        [ProtoMember(1)] public ushort X;
        [ProtoMember(2)] public ushort Y;
        [ProtoMember(3)] public ushort Z;

        public Vector3UShort(ushort i, ushort i1, ushort i2)
        {
            X = i;
            Y = i1;
            Z = i2;
        }
    }
}