using System;
using System.Collections.Generic;
using ProtoBuf;

namespace WeCraft.Core.C2S
{
    /// <summary>
    /// 是客户端连接时候发送的,如果无法发送,就会断开
    /// </summary>
    [Serializable]
    [ProtoContract]
    public class C2S_PlayerLogin
    {
        [ProtoMember(1)]
        public string JWT_Token;
        [ProtoMember(2)]
        public List<string> Mods;
    }
}