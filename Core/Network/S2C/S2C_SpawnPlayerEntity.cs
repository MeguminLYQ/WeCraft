using System;
using ProtoBuf;

namespace WeCraft.Core.S2C
{
    [Serializable]
    [ProtoContract]
    public class S2C_SpawnPlayerEntity: S2C_SpawnEntity
    {
        [ProtoMember(4)] 
        public string Character;
    }
}