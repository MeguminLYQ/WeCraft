using System;

namespace WeCraft.Core.Utility
{
    public static class TypeHelper
    {
        public static bool IsParentType(Type parent, Type child)
        {
            return parent.IsAssignableFrom(child);
        }
    }
}