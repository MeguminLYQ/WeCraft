using System;
using WeCraft.Core.C2S;
using WeCraft.Core.Entity;
using WeCraft.Core.Math;
using WeCraft.Core.Network;
using WeCraft.Core.Registery;

namespace WeCraft.Core.World
{
    public class World: IEquatable<World>
    {
        //读取了的所有区块
        public Chunk[] LoadedChunks { get; private set; } 
        protected Vector3 _spawnPoint=default;
        public string Name { get; set; }
        public WorldDefinition Definition { get; }
        public World Value { get; }

        public int MaxEntities;
        public int MaxChunks;
        /// <summary>
        /// 
        /// </summary>
        public string Type { get; } = "default";
        
        
        public Vector3 SpawnPoint => _spawnPoint;
        public Random RandomGenerator { get; private set; }

        private uint currentId = 0;
        
        protected World()
        {
            Value = this;
        }

        public World(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// 可以注册,世界类型,来选择对应的世界,对应Setting.WorldType
        /// </summary>
        /// <returns></returns>
        public virtual uint GetNextEntityId()
        {
            return currentId++;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="customPacket">if customed packet it won't send S2C_EntitySpawn if any extra data is need u probably need custom packet for spawning an entity</param>
        /// <returns></returns>
        public bool AddEntity(Entity.Entity entity,bool customPacket=false)
        {
            return false;
        }

        public bool AddEntity(string mod, string type, Vector3 pos, bool customPacket = false)
        {
            Entity.Entity entity = null;
            return AddEntity(entity,customPacket);
        }
        public bool AddPlayer(C2S_PlayerProfile profile,out Player player)
        {
            return AddPlayer(profile,SpawnPoint,out player);
        }
        public bool AddPlayer(C2S_PlayerProfile profile,Vector3 pos,out Player player)
        {
            player = new Player(profile, GetNextEntityId())
            {
                Location = new Location()
                {
                    Position = pos,
                    World = this
                }
            };
            return AddEntity(player,true);
        }

        /// <summary>
        /// 检测地点是否安全.
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        public bool IsPositionSafe(Vector3Int pos,BoundsUShort range=default)
        {
            return true;
        }


        public bool Equals(World other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((World)obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}