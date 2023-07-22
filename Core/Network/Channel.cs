using System.Collections.Generic;
using WeCraft.Core;
using WeCraft.Core.Exception;

namespace WeCraft.Core.Network
{
    public class Channel
    {
        public string Name;
        public ushort Id;
        protected Dictionary<ushort, List<NetworkHandler.Handle>> Handlers=new Dictionary<ushort, List<NetworkHandler.Handle>>();

        protected Channel(){}

        public Channel(string name, ushort id)
        {
            this.Name = name;
            this.Id = id;
        }
        
        public bool RegisterHandler(ushort id,NetworkHandler.Handle handle)
        {
            if (Handlers.TryGetValue(id, out List<NetworkHandler.Handle> handles))
            {
                if (!handles.Contains(handle))
                {
                    handles.Add(handle);
                    return true;
                }
            }
            else
            {
                Handlers.Add(id,new List<NetworkHandler.Handle>(){handle});
            }
            return false;
        }

        public bool RemoveHandler(ushort id, NetworkHandler.Handle handle)
        {
            if (Handlers.TryGetValue(id, out List<NetworkHandler.Handle> handles))
            { 
                handles.Remove(handle);
                return true; 
            }
            return false;
        }
        
        /// <summary>
        /// 处理包可能出错,所以尽可能的先检测是否有,因为throw很消耗性能
        /// </summary>
        /// <param name="packId"></param>
        /// <param name="data"></param>
        /// <exception cref="E"></exception>
        public bool HandlePacket(ushort clientId,ushort packId, byte[] data)
        {
            bool hasHandler = Handlers.TryGetValue(packId, out var handles);
            if (!hasHandler)
                return false;
            for (var i = Handlers.Count - 1; i >= 0; i--)
            {
                if (handles[i] != null)
                {
                    handles[i].Invoke(clientId,data);
                }
            } 
            return true;
        }

        public bool HandlePacket(ushort clientId,PackId packId, byte[] data)
        {
            return HandlePacket(clientId,packId,data);   
        }
    }
}