using System;

namespace WeCraft.Core.Mod
{
    //所有地方只会存一个,所以用class
    public class ModId: IEquatable<ModId>
    {
        public string Domain;
        
        public ModId()
        {
            
        }
        public static bool operator ==(ModId id,ModId id2)
        {
            return id.Equals(id2);
        }

        public static bool operator !=(ModId id, ModId id2)
        {
            return !(id == id2);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            return ToString().Equals(obj.ToString());
        }

        public override string ToString()
        {
            return Domain;
        }

        public bool Equals(ModId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Domain == other.Domain;
        }

        public override int GetHashCode()
        {
            return (Domain != null ? Domain.GetHashCode() : 0);
        }
    }
}