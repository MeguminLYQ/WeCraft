using System;
using ProtoBuf;

namespace WeCraft.Core.S2C
{
    [Serializable]
    [ProtoContract]
    public class S2C_SpawnEntity
    {
        [ProtoMember(1)]
        public ushort Id;
        [ProtoMember(2)] 
        public string Group;
        [ProtoMember(3)] 
        public string Type; 
    }
}