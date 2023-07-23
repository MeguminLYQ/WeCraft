using System;
using ProtoBuf;
using WeCraft.Core.Math;

namespace WeCraft.Core.C2S
{
    
    [Serializable]
    [ProtoContract]
    public class C2S_PlayerMovement
    {
        [ProtoMember(1)]
        public ushort Id;
        [ProtoMember(2)]
        public Vector3 DeltaPos;
        [ProtoMember(3)] 
        public float Yaw;

        [ProtoMember(4)] 
        public byte Tick;
    }
}