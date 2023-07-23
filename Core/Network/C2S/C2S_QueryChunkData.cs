using System;
using ProtoBuf;
using WeCraft.Core.Math;

namespace WeCraft.Core.C2S
{
    
    [Serializable]
    [ProtoContract]
    public class C2S_QueryChunkData
    {
        [ProtoMember(1)]
        private Vector3UShort Coordinate;
    }
}