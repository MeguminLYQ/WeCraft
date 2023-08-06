using System;
using System.Collections.Generic;
using NLog; 
using WeCraft.Core.Mod;
using WeCraft.Core.Utility;

namespace WeCraft.Core.Event
{
    public class EventBus
    {
        protected const int CapacityPrediction = 2;
        protected const int DefaultCapacity = 16;
        protected static ILogger Logger = WeCraftCore.CoreImpl.LoggerImpl;
        protected static readonly Dictionary<Type, List<EventHandlerBase>> EventTable =
            new Dictionary<Type, List<EventHandlerBase>>();
       
         
        public static void Register<T>(ModId id,ExecuteDelegate<T> obj) where T:EventBase
        {
            RegisterInternal(id,obj,(byte)EventPriority.Normal);
        }

        //注册应该
        private static void RegisterInternal<T>(ModId id, ExecuteDelegate<T> obj,byte priority) where T:EventBase
        {
            var eventHandler = new Core.Event.EventHandler<T>(id, obj,priority);
            var eventType = typeof(T);
            List<EventHandlerBase> handlers;
            if (EventTable.TryGetValue(eventType, out var value))
            {
                handlers = value;
            }
            else
            {
                handlers = new List<EventHandlerBase>(DefaultCapacity);
            } 
            if (handlers.Count <= handlers.Capacity - 1)
            {
                handlers.Capacity += CapacityPrediction;
            }
            handlers.Add(eventHandler);
            handlers.Sort((a,b)=>a.Priority.CompareTo(b.Priority));
        }
        
        public static void Post<T>(T obj) where T:EventBase
        {
            var handlers=EventTable[typeof(T)];
            PostInternal(handlers,obj);
        }

        public static void PostRecursive<T>(T obj) where T:EventBase
        {
            foreach (var keyValuePair in EventTable)
            {
                bool shouldExecute=TypeHelper.IsParentType(typeof(T), keyValuePair.Key);
                PostInternal(keyValuePair.Value,obj);
            }
        }
        private static void PostInternal<T>(IEnumerable<EventHandlerBase> handlers,T @event) where T : EventBase
        {
            if(handlers==null)
                return;
            foreach (var eventHandlerBase in handlers)
            {
                if(eventHandlerBase is EventHandler<T> handler)
                {
                    handler.Execute(@event);
                }
                else
                {
                    Logger.Error($"执行事件 {typeof(T).Name} 时,有不正确的handler被注册了");
                }
            }   
        } 
    }
}