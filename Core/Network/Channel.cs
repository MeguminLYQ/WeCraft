using System.Collections.Generic;
using Core;
using Core.Exception;

namespace WeCraftServer.Network
{
    public class Channel
    {
        public string Name;
        public uint Id;
        public Dictionary<PackId, List<NetworkHandler.Handle>> Handlers=new Dictionary<PackId, List<NetworkHandler.Handle>>();

        protected Channel(){}

        public Channel(string name, uint id)
        {
            this.Name = name;
            this.Id = id;
        }
        
        public void RegisterHandler()
        {
        }
        /// <summary>
        /// 处理包可能出错,所以尽可能的先检测是否有,因为throw很消耗性能
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <exception cref="E"></exception>
        public bool HandlePacket(PackId id, byte[] data)
        {
            bool hasHandler = Handlers.TryGetValue(id, out var handles);
            if (!hasHandler)
                return false;
            for (var i = Handlers.Count - 1; i >= 0; i--)
            {
                if (handles[i] != null)
                {
                    handles[i].Invoke(data);
                }
            } 
            return true;
        }

        public bool HandlePacket(uint id, byte[] data)
        {
            return HandlePacket((PackId)id,data);   
        }
    }
}