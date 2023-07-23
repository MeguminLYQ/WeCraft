using System;
using ProtoBuf;

namespace WeCraft.Core.Math
{
    [Serializable]
    [ProtoContract]
    public struct Vector3
    {
        [ProtoMember(1)] public float X;
        [ProtoMember(2)] public float Y;
        [ProtoMember(3)] public float Z;

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}