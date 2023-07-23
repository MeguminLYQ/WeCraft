using System;
using ProtoBuf;

namespace WeCraft.Core.Math
{
    [Serializable]
    [ProtoContract]
    public struct Vector3Int
    {
        [ProtoMember(1)] public int X;
        [ProtoMember(2)] public int Y;
        [ProtoMember(3)] public int Z;

        public Vector3Int(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}