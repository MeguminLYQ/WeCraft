using System;

namespace WeCraft.Core.Registery
{
    public interface IRegistrable<T>:IEquatable<T>
    {
        T Value { get; }
        public string Type { get; }
    }
}