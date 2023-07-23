using ProtoBuf;
using WeCraft.Core.Math;

namespace WeCraft.Core.S2C
{
    [ProtoContract]
    public class S2C_QueryChunkData
    {
        [ProtoMember(1)]
        public Vector3UShort Coordinate;
        
    }

    public class BlockInfo
    {
        
    }

    public class EntityInfo
    {
        
    }
}