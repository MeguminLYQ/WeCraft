using System.Collections.Generic;
using WeCraft.Core.Mod;

namespace WeCraft.Core.Registery
{
    //注册表
    //物品注册表{ModA{物品A,物品B},ModB{物品A,物品B}}
    //
    public abstract class Registery<T>
    {
        protected Dictionary<ModId, HashSet<IRegistrable<T>>> _table=new Dictionary<ModId,HashSet<IRegistrable<T>>>();

        public bool Register(ModId id, IRegistrable<T> value)
        {
            if (!_table.TryGetValue(id,out HashSet<IRegistrable<T>> set))
            {
                _table[id] = new HashSet<IRegistrable<T>>() { value };
                return true;
            }

            if (set.Contains(value))
            {
                return false;
            }

            set.Add(value);
            return true;
        }
    }
}