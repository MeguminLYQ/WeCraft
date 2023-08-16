using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using WeCraft.Core.Mod;

namespace WeCraft.Core.Registery
{
    //注册表
    //物品注册表{ModA{物品A,物品B},ModB{物品A,物品B}}
    //
    public abstract class Registery<T>
    {
        public const string RegixPattern = "[0-9a-z-A-Z]+";
        protected Dictionary<string, HashSet<IRegistrable<T>>> _table=new Dictionary<string,HashSet<IRegistrable<T>>>();

        public bool Register(string id, IRegistrable<T> value)
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

        public bool Get(string id, string type,out T target)
        {
            target = default;
            bool hasRecord = _table.TryGetValue(id, out HashSet<IRegistrable<T>> targetList);
            if (!hasRecord)
                return false;
            foreach (var registrable in targetList)
            {
                if (registrable.Type == type)
                {
                    target = registrable.Value;
                    return true;
                }
            }
            return false;
        }

        public bool Get(ModId id, string type, out T target)
        {
            return Get(id.ToString(), type, out target);
        }

        public bool Get(string pattern,out T target)
        {
            target = default;
            int index = pattern.IndexOf(":", StringComparison.Ordinal); // 获取第一个":"的索引位置
            try
            {
                string part1 = pattern.Substring(0, index); // 获取第一个":"之前的部分
                string part2 = pattern.Substring(index + 1);
                
                if (!MatchPattern(part1))
                {
                    WeCraftCore.CoreImpl.LoggerImpl.Error($"Pattern Dont Mod Doman {part1} dont Match");
                    return false;
                }
                if (!MatchPattern(part2))
                {
                    WeCraftCore.CoreImpl.LoggerImpl.Error($"Pattern {typeof(T).Name} type {part2} Dont Match ");
                    return false;
                }

                return Get(part1,part2,out target);
            }
            catch (System.Exception e)
            {
                WeCraftCore.CoreImpl.LoggerImpl.Error($"Split string error");
                WeCraftCore.CoreImpl.LoggerImpl.Error(e);
                return false;
            }
        }

        public bool MatchPattern(string str)
        {
            return Regex.IsMatch(str,RegixPattern);
        }
    }
}