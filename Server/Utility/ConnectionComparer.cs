using System.Collections.Generic;
using Riptide;

namespace WeCraftServer.Utility
{
    public struct ConnectionComparer:IEqualityComparer<Connection>
    {
        public bool Equals(Connection x, Connection y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            return x.Id==y.Id;
        }

        public int GetHashCode(Connection obj)
        {
            unchecked
            {
                return obj.Id.GetHashCode();
            }
        }
    }
}