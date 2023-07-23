using System;
using System.Collections.Generic;

namespace WeCraft.Core.Utility
{

    public static class GenericObjectPool
    {
        private static Dictionary<Type, object> s_GenericPool = new Dictionary<Type, object>();

        public static T Get<T>()
        {
            object obj;
            if (GenericObjectPool.s_GenericPool.TryGetValue(typeof (T), out obj))
            {
                Stack<T> objStack = obj as Stack<T>;
                if (objStack.Count > 0)
                    return objStack.Pop();
            }
            if (!typeof (T).IsArray)
                return Activator.CreateInstance<T>();
            return (T) Activator.CreateInstance(typeof (T), (object) 0);
        }

        public static void Return<T>(T obj)
        {
            object obj1;
            if (GenericObjectPool.s_GenericPool.TryGetValue(typeof (T), out obj1))
            {
                (obj1 as Stack<T>).Push(obj);
            }
            else
            {
                Stack<T> objStack = new Stack<T>();
                objStack.Push(obj);
                GenericObjectPool.s_GenericPool.Add(typeof (T), (object) objStack);
            }
        }
    }
}