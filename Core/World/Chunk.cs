using System.Collections.Generic;
using WeCraft.Core.Math;

namespace WeCraft.Core.World
{
    public class Chunk
    {
        public HashSet<Entity.Entity> Entities;
        public bool Loaded;
        /// <summary>
        /// 是否会被激活
        /// 在玩家视野距离内会被激活,进行tick操作,反之则不会
        /// </summary>
        public bool Active;

        public Vector3UShort Coordinate;
    }
}