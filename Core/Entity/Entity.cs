using System;
using WeCraft.Core.Math;
using WeCraft.Core.Mod;

namespace WeCraft.Core.Entity
{
    public class Entity:IEquatable<Entity>
    {
        protected uint m_Id;

        /// <summary>
        /// 实体的Id,根据加载的顺序来决定的,用于网络传输
        /// </summary>
        public uint Id => m_Id;
        protected string m_Name;
        public virtual string Name { get=>m_Name;
            set { m_Name = value; }
        }
        public Location Location;
        /// <summary>
        /// 实体的位移id,会保存在地图中
        /// </summary>
        public Guid Guid; 
        public bool CanSave { get; protected set; }

        protected Entity()
        {
        }

        protected Entity(uint id)
        {
            this.m_Id = id;
        } 
        public bool Equals(Entity other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Guid.Equals(other.Guid);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Entity)obj);
        }

        public override int GetHashCode()
        {
            return Guid.GetHashCode();
        }
    }
}