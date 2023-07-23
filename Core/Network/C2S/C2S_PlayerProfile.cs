using System;
using System.Collections.Generic;
using ProtoBuf;

namespace WeCraft.Core.C2S
{
    
    [Serializable]
    [ProtoContract]
    public class C2S_PlayerProfile
    {
        [ProtoMember(1)]
        public string Guid;
        [ProtoMember(2)]
        public string Name;
        [ProtoMember(3)]
        public string Character;
    }
}