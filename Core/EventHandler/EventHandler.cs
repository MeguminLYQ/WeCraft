using System;
using System.Collections.Generic;
using System.Diagnostics;
using WeCraft.Core.Utility;

namespace WeCraft.Core.EventHandler
{
  public class EventHandler 
  {
    private static Dictionary<object, Dictionary<string, List<InvokableActionBase>>> s_EventTable = new Dictionary<object, Dictionary<string, List<InvokableActionBase>>>();
    private static Dictionary<string, List<InvokableActionBase>> s_GlobalEventTable = new Dictionary<string, List<InvokableActionBase>>();

    private static void RegisterEvent(string eventName, InvokableActionBase invokableAction)
    {
      List<InvokableActionBase> invokableActionBaseList;
      if (EventHandler.s_GlobalEventTable.TryGetValue(eventName, out invokableActionBaseList))
        invokableActionBaseList.Add(invokableAction);
      else
        EventHandler.s_GlobalEventTable.Add(eventName, new List<InvokableActionBase>()
        {
          invokableAction
        });
    }

    private static void RegisterEvent(
      object obj,
      string eventName,
      InvokableActionBase invokableAction)
    {
      Dictionary<string, List<InvokableActionBase>> dictionary;
      if (!EventHandler.s_EventTable.TryGetValue(obj, out dictionary))
      {
        dictionary = new Dictionary<string, List<InvokableActionBase>>();
        EventHandler.s_EventTable.Add(obj, dictionary);
      }
      List<InvokableActionBase> invokableActionBaseList;
      if (dictionary.TryGetValue(eventName, out invokableActionBaseList))
        invokableActionBaseList.Add(invokableAction);
      else
        dictionary.Add(eventName, new List<InvokableActionBase>()
        {
          invokableAction
        });
    }

    private static List<InvokableActionBase> GetActionList(string eventName)
    {
      List<InvokableActionBase> invokableActionBaseList;
      return EventHandler.s_GlobalEventTable.TryGetValue(eventName, out invokableActionBaseList) ? invokableActionBaseList : (List<InvokableActionBase>) null;
    }

    private static void CheckForEventRemoval(string eventName, List<InvokableActionBase> actionList)
    {
      if (actionList.Count != 0)
        return;
      EventHandler.s_GlobalEventTable.Remove(eventName);
    }

    private static List<InvokableActionBase> GetActionList(object obj, string eventName)
    {
      Dictionary<string, List<InvokableActionBase>> dictionary;
      List<InvokableActionBase> invokableActionBaseList;
      return EventHandler.s_EventTable.TryGetValue(obj, out dictionary) && dictionary.TryGetValue(eventName, out invokableActionBaseList) ? invokableActionBaseList : (List<InvokableActionBase>) null;
    }

    private static void CheckForEventRemoval(
      object obj,
      string eventName,
      List<InvokableActionBase> actionList)
    {
      Dictionary<string, List<InvokableActionBase>> dictionary;
      if (actionList.Count != 0 || !EventHandler.s_EventTable.TryGetValue(obj, out dictionary))
        return;
      dictionary.Remove(eventName);
      if (dictionary.Count != 0)
        return;
      EventHandler.s_EventTable.Remove(obj);
    }

    public static void RegisterEvent(string eventName, Action action)
    {
      InvokableAction invokableAction = GenericObjectPool.Get<InvokableAction>();
      invokableAction.Initialize(action);
      EventHandler.RegisterEvent(eventName, (InvokableActionBase) invokableAction);
    }

    public static void RegisterEvent(object obj, string eventName, Action action)
    {
      InvokableAction invokableAction = GenericObjectPool.Get<InvokableAction>();
      invokableAction.Initialize(action);
      EventHandler.RegisterEvent(obj, eventName, (InvokableActionBase) invokableAction);
    }

    public static void RegisterEvent<T1>(string eventName, Action<T1> action)
    {
      InvokableAction<T1> invokableAction = GenericObjectPool.Get<InvokableAction<T1>>();
      invokableAction.Initialize(action);
      EventHandler.RegisterEvent(eventName, (InvokableActionBase) invokableAction);
    }

    public static void RegisterEvent<T1>(object obj, string eventName, Action<T1> action)
    {
      InvokableAction<T1> invokableAction = GenericObjectPool.Get<InvokableAction<T1>>();
      invokableAction.Initialize(action);
      EventHandler.RegisterEvent(obj, eventName, (InvokableActionBase) invokableAction);
    }

    public static void RegisterEvent<T1, T2>(string eventName, Action<T1, T2> action)
    {
      InvokableAction<T1, T2> invokableAction = GenericObjectPool.Get<InvokableAction<T1, T2>>();
      invokableAction.Initialize(action);
      EventHandler.RegisterEvent(eventName, (InvokableActionBase) invokableAction);
    }

    public static void RegisterEvent<T1, T2>(object obj, string eventName, Action<T1, T2> action)
    {
      InvokableAction<T1, T2> invokableAction = GenericObjectPool.Get<InvokableAction<T1, T2>>();
      invokableAction.Initialize(action);
      EventHandler.RegisterEvent(obj, eventName, (InvokableActionBase) invokableAction);
    }

    public static void RegisterEvent<T1, T2, T3>(string eventName, Action<T1, T2, T3> action)
    {
      InvokableAction<T1, T2, T3> invokableAction = GenericObjectPool.Get<InvokableAction<T1, T2, T3>>();
      invokableAction.Initialize(action);
      EventHandler.RegisterEvent(eventName, (InvokableActionBase) invokableAction);
    }

    public static void RegisterEvent<T1, T2, T3>(
      object obj,
      string eventName,
      Action<T1, T2, T3> action)
    {
      InvokableAction<T1, T2, T3> invokableAction = GenericObjectPool.Get<InvokableAction<T1, T2, T3>>();
      invokableAction.Initialize(action);
      EventHandler.RegisterEvent(obj, eventName, (InvokableActionBase) invokableAction);
    }

    public static void RegisterEvent<T1, T2, T3, T4>(
      string eventName,
      Action<T1, T2, T3, T4> action)
    {
      InvokableAction<T1, T2, T3, T4> invokableAction = GenericObjectPool.Get<InvokableAction<T1, T2, T3, T4>>();
      invokableAction.Initialize(action);
      EventHandler.RegisterEvent(eventName, (InvokableActionBase) invokableAction);
    }

    public static void RegisterEvent<T1, T2, T3, T4>(
      object obj,
      string eventName,
      Action<T1, T2, T3, T4> action)
    {
      InvokableAction<T1, T2, T3, T4> invokableAction = GenericObjectPool.Get<InvokableAction<T1, T2, T3, T4>>();
      invokableAction.Initialize(action);
      EventHandler.RegisterEvent(obj, eventName, (InvokableActionBase) invokableAction);
    }

    public static void RegisterEvent<T1, T2, T3, T4, T5>(
      string eventName,
      Action<T1, T2, T3, T4, T5> action)
    {
      InvokableAction<T1, T2, T3, T4, T5> invokableAction = GenericObjectPool.Get<InvokableAction<T1, T2, T3, T4, T5>>();
      invokableAction.Initialize(action);
      EventHandler.RegisterEvent(eventName, (InvokableActionBase) invokableAction);
    }

    public static void RegisterEvent<T1, T2, T3, T4, T5>(
      object obj,
      string eventName,
      Action<T1, T2, T3, T4, T5> action)
    {
      InvokableAction<T1, T2, T3, T4, T5> invokableAction = GenericObjectPool.Get<InvokableAction<T1, T2, T3, T4, T5>>();
      invokableAction.Initialize(action);
      EventHandler.RegisterEvent(obj, eventName, (InvokableActionBase) invokableAction);
    }

    public static void RegisterEvent<T1, T2, T3, T4, T5, T6>(
      string eventName,
      Action<T1, T2, T3, T4, T5, T6> action)
    {
      InvokableAction<T1, T2, T3, T4, T5, T6> invokableAction = GenericObjectPool.Get<InvokableAction<T1, T2, T3, T4, T5, T6>>();
      invokableAction.Initialize(action);
      EventHandler.RegisterEvent(eventName, (InvokableActionBase) invokableAction);
    }

    public static void RegisterEvent<T1, T2, T3, T4, T5, T6>(
      object obj,
      string eventName,
      Action<T1, T2, T3, T4, T5, T6> action)
    {
      InvokableAction<T1, T2, T3, T4, T5, T6> invokableAction = GenericObjectPool.Get<InvokableAction<T1, T2, T3, T4, T5, T6>>();
      invokableAction.Initialize(action);
      EventHandler.RegisterEvent(obj, eventName, (InvokableActionBase) invokableAction);
    }

    private static T TryGetValidAction<T>(
      string eventName,
      int index,
      List<InvokableActionBase> actions)
      where T : InvokableActionBase
    {
      if (index < 0 || index >= actions.Count)
      {
        WeCraftCore.CoreImpl.LoggerImpl.Error((object) string.Format("Error: The event '{0}' is out of range with an index value of {1} out of {2}.", (object) eventName, (object) index, (object) actions.Count));
        return (T) null;
      }
      if (actions[index] is T)
        return actions[index] as T;
      WeCraftCore.CoreImpl.LoggerImpl.Error((object) string.Format("Error: The event '{0}' with type {1} but {2} was expected.", (object) eventName, (object) actions[index].GetType(), (object) typeof (T)));
      return (T) null;
    }

    public static void ExecuteEvent(string eventName)
    {
      List<InvokableActionBase> actionList = EventHandler.GetActionList(eventName);
      if (actionList == null)
        return;
      int count = actionList.Count;
      int num = 0;
      for (int index1 = 0; index1 + num < count; ++index1)
      {
        int index2 = index1 + num;
        InvokableAction validAction;
        if (index2 >= 0 && index2 < actionList.Count && (validAction = EventHandler.TryGetValidAction<InvokableAction>(eventName, index2, actionList)) != null)
        {
          validAction.Invoke();
          if (count != actionList.Count)
          {
            num = num + actionList.Count - count;
            count = actionList.Count;
          }
        }
      }
    }

    public static void ExecuteEvent(object obj, string eventName)
    {
      List<InvokableActionBase> actionList = EventHandler.GetActionList(obj, eventName);
      if (actionList == null || actionList.Count <= 0)
        return;
      int count = actionList.Count;
      int num = 0;
      for (int index1 = 0; index1 + num < count; ++index1)
      {
        int index2 = index1 + num;
        InvokableAction validAction;
        if (index2 >= 0 && index2 < actionList.Count && (validAction = EventHandler.TryGetValidAction<InvokableAction>(eventName, index2, actionList)) != null)
        {
          validAction.Invoke();
          if (actionList.Count < count)
          {
            num = num + actionList.Count - count;
            count = actionList.Count;
          }
        }
      }
    }

    public static void ExecuteEvent<T1>(string eventName, T1 arg1)
    {
      List<InvokableActionBase> actionList = EventHandler.GetActionList(eventName);
      if (actionList == null)
        return;
      int count = actionList.Count;
      int num = 0;
      for (int index1 = 0; index1 + num < count; ++index1)
      {
        int index2 = index1 + num;
        InvokableAction<T1> validAction;
        if (index2 >= 0 && index2 < actionList.Count && (validAction = EventHandler.TryGetValidAction<InvokableAction<T1>>(eventName, index2, actionList)) != null)
        {
          validAction.Invoke(arg1);
          if (actionList.Count < count)
          {
            num = num + actionList.Count - count;
            count = actionList.Count;
          }
        }
      }
    }

    public static void ExecuteEvent<T1>(object obj, string eventName, T1 arg1)
    {
      List<InvokableActionBase> actionList = EventHandler.GetActionList(obj, eventName);
      if (actionList == null)
        return;
      int count = actionList.Count;
      int num = 0;
      for (int index1 = 0; index1 + num < count; ++index1)
      {
        int index2 = index1 + num;
        InvokableAction<T1> validAction;
        if (index2 >= 0 && index2 < actionList.Count && (validAction = EventHandler.TryGetValidAction<InvokableAction<T1>>(eventName, index2, actionList)) != null)
        {
          validAction.Invoke(arg1);
          if (actionList.Count < count)
          {
            num = num + actionList.Count - count;
            count = actionList.Count;
          }
        }
      }
    }

    public static void ExecuteEvent<T1, T2>(string eventName, T1 arg1, T2 arg2)
    {
      List<InvokableActionBase> actionList = EventHandler.GetActionList(eventName);
      if (actionList == null)
        return;
      int count = actionList.Count;
      int num = 0;
      for (int index1 = 0; index1 + num < count; ++index1)
      {
        int index2 = index1 + num;
        InvokableAction<T1, T2> validAction;
        if (index2 >= 0 && index2 < actionList.Count && (validAction = EventHandler.TryGetValidAction<InvokableAction<T1, T2>>(eventName, index2, actionList)) != null)
        {
          validAction.Invoke(arg1, arg2);
          if (actionList.Count < count)
          {
            num = num + actionList.Count - count;
            count = actionList.Count;
          }
        }
      }
    }

    public static void ExecuteEvent<T1, T2>(object obj, string eventName, T1 arg1, T2 arg2)
    {
      List<InvokableActionBase> actionList = EventHandler.GetActionList(obj, eventName);
      if (actionList == null)
        return;
      int count = actionList.Count;
      int num = 0;
      for (int index1 = 0; index1 + num < count; ++index1)
      {
        int index2 = index1 + num;
        InvokableAction<T1, T2> validAction;
        if (index2 >= 0 && index2 < actionList.Count && (validAction = EventHandler.TryGetValidAction<InvokableAction<T1, T2>>(eventName, index2, actionList)) != null)
        {
          validAction.Invoke(arg1, arg2);
          if (actionList.Count < count)
          {
            num = num + actionList.Count - count;
            count = actionList.Count;
          }
        }
      }
    }

    public static void ExecuteEvent<T1, T2, T3>(string eventName, T1 arg1, T2 arg2, T3 arg3)
    {
      List<InvokableActionBase> actionList = EventHandler.GetActionList(eventName);
      if (actionList == null)
        return;
      int count = actionList.Count;
      int num = 0;
      for (int index1 = 0; index1 + num < count; ++index1)
      {
        int index2 = index1 + num;
        InvokableAction<T1, T2, T3> validAction;
        if (index2 >= 0 && index2 < actionList.Count && (validAction = EventHandler.TryGetValidAction<InvokableAction<T1, T2, T3>>(eventName, index2, actionList)) != null)
        {
          validAction.Invoke(arg1, arg2, arg3);
          if (actionList.Count < count)
          {
            num = num + actionList.Count - count;
            count = actionList.Count;
          }
        }
      }
    }

    public static void ExecuteEvent<T1, T2, T3>(
      object obj,
      string eventName,
      T1 arg1,
      T2 arg2,
      T3 arg3)
    {
      List<InvokableActionBase> actionList = EventHandler.GetActionList(obj, eventName);
      if (actionList == null)
        return;
      int count = actionList.Count;
      int num = 0;
      for (int index1 = 0; index1 + num < count; ++index1)
      {
        int index2 = index1 + num;
        InvokableAction<T1, T2, T3> validAction;
        if (index2 >= 0 && index2 < actionList.Count && (validAction = EventHandler.TryGetValidAction<InvokableAction<T1, T2, T3>>(eventName, index2, actionList)) != null)
        {
          validAction.Invoke(arg1, arg2, arg3);
          if (actionList.Count < count)
          {
            num = num + actionList.Count - count;
            count = actionList.Count;
          }
        }
      }
    }

    public static void ExecuteEvent<T1, T2, T3, T4>(
      string eventName,
      T1 arg1,
      T2 arg2,
      T3 arg3,
      T4 arg4)
    {
      List<InvokableActionBase> actionList = EventHandler.GetActionList(eventName);
      if (actionList == null)
        return;
      int count = actionList.Count;
      int num = 0;
      for (int index1 = 0; index1 + num < count; ++index1)
      {
        int index2 = index1 + num;
        InvokableAction<T1, T2, T3, T4> validAction;
        if (index2 >= 0 && index2 < actionList.Count && (validAction = EventHandler.TryGetValidAction<InvokableAction<T1, T2, T3, T4>>(eventName, index2, actionList)) != null)
        {
          validAction.Invoke(arg1, arg2, arg3, arg4);
          if (actionList.Count < count)
          {
            num = num + actionList.Count - count;
            count = actionList.Count;
          }
        }
      }
    }

    public static void ExecuteEvent<T1, T2, T3, T4>(
      object obj,
      string eventName,
      T1 arg1,
      T2 arg2,
      T3 arg3,
      T4 arg4)
    {
      List<InvokableActionBase> actionList = EventHandler.GetActionList(obj, eventName);
      if (actionList == null)
        return;
      int count = actionList.Count;
      int num = 0;
      for (int index1 = 0; index1 + num < count; ++index1)
      {
        int index2 = index1 + num;
        InvokableAction<T1, T2, T3, T4> validAction;
        if (index2 >= 0 && index2 < actionList.Count && (validAction = EventHandler.TryGetValidAction<InvokableAction<T1, T2, T3, T4>>(eventName, index2, actionList)) != null)
        {
          validAction.Invoke(arg1, arg2, arg3, arg4);
          if (actionList.Count < count)
          {
            num = num + actionList.Count - count;
            count = actionList.Count;
          }
        }
      }
    }

    public static void ExecuteEvent<T1, T2, T3, T4, T5>(
      string eventName,
      T1 arg1,
      T2 arg2,
      T3 arg3,
      T4 arg4,
      T5 arg5)
    {
      List<InvokableActionBase> actionList = EventHandler.GetActionList(eventName);
      if (actionList == null)
        return;
      int count = actionList.Count;
      int num = 0;
      for (int index1 = 0; index1 + num < count; ++index1)
      {
        int index2 = index1 + num;
        InvokableAction<T1, T2, T3, T4, T5> validAction;
        if (index2 >= 0 && index2 < actionList.Count && (validAction = EventHandler.TryGetValidAction<InvokableAction<T1, T2, T3, T4, T5>>(eventName, index2, actionList)) != null)
        {
          validAction.Invoke(arg1, arg2, arg3, arg4, arg5);
          if (actionList.Count < count)
          {
            num = num + actionList.Count - count;
            count = actionList.Count;
          }
        }
      }
    }

    public static void ExecuteEvent<T1, T2, T3, T4, T5>(
      object obj,
      string eventName,
      T1 arg1,
      T2 arg2,
      T3 arg3,
      T4 arg4,
      T5 arg5)
    {
      List<InvokableActionBase> actionList = EventHandler.GetActionList(obj, eventName);
      if (actionList == null)
        return;
      int count = actionList.Count;
      int num = 0;
      for (int index1 = 0; index1 + num < count; ++index1)
      {
        int index2 = index1 + num;
        InvokableAction<T1, T2, T3, T4, T5> validAction;
        if (index2 >= 0 && index2 < actionList.Count && (validAction = EventHandler.TryGetValidAction<InvokableAction<T1, T2, T3, T4, T5>>(eventName, index2, actionList)) != null)
        {
          validAction.Invoke(arg1, arg2, arg3, arg4, arg5);
          if (actionList.Count < count)
          {
            num = num + actionList.Count - count;
            count = actionList.Count;
          }
        }
      }
    }

    public static void ExecuteEvent<T1, T2, T3, T4, T5, T6>(
      string eventName,
      T1 arg1,
      T2 arg2,
      T3 arg3,
      T4 arg4,
      T5 arg5,
      T6 arg6)
    {
      List<InvokableActionBase> actionList = EventHandler.GetActionList(eventName);
      if (actionList == null)
        return;
      int count = actionList.Count;
      int num = 0;
      for (int index1 = 0; index1 + num < count; ++index1)
      {
        int index2 = index1 + num;
        InvokableAction<T1, T2, T3, T4, T5, T6> validAction;
        if (index2 >= 0 && index2 < actionList.Count && (validAction = EventHandler.TryGetValidAction<InvokableAction<T1, T2, T3, T4, T5, T6>>(eventName, index2, actionList)) != null)
        {
          validAction.Invoke(arg1, arg2, arg3, arg4, arg5, arg6);
          if (actionList.Count < count)
          {
            num = num + actionList.Count - count;
            count = actionList.Count;
          }
        }
      }
    }

    public static void ExecuteEvent<T1, T2, T3, T4, T5, T6>(
      object obj,
      string eventName,
      T1 arg1,
      T2 arg2,
      T3 arg3,
      T4 arg4,
      T5 arg5,
      T6 arg6)
    {
      List<InvokableActionBase> actionList = EventHandler.GetActionList(obj, eventName);
      if (actionList == null)
        return;
      int count = actionList.Count;
      int num = 0;
      for (int index1 = 0; index1 + num < count; ++index1)
      {
        int index2 = index1 + num;
        InvokableAction<T1, T2, T3, T4, T5, T6> validAction;
        if (index2 >= 0 && index2 < actionList.Count && (validAction = EventHandler.TryGetValidAction<InvokableAction<T1, T2, T3, T4, T5, T6>>(eventName, index2, actionList)) != null)
        {
          validAction.Invoke(arg1, arg2, arg3, arg4, arg5, arg6);
          if (actionList.Count < count)
          {
            num = num + actionList.Count - count;
            count = actionList.Count;
          }
        }
      }
    }

    public static void UnregisterEvent(string eventName, Action action)
    {
      List<InvokableActionBase> actionList = EventHandler.GetActionList(eventName);
      if (actionList == null)
        return;
      for (int index = 0; index < actionList.Count; ++index)
      {
        InvokableAction invokableAction = actionList[index] as InvokableAction;
        if (invokableAction.IsAction(action))
        {
          GenericObjectPool.Return<InvokableAction>(invokableAction);
          actionList.RemoveAt(index);
          break;
        }
      }
      EventHandler.CheckForEventRemoval(eventName, actionList);
    }

    public static void UnregisterEvent(object obj, string eventName, Action action)
    {
      List<InvokableActionBase> actionList = EventHandler.GetActionList(obj, eventName);
      if (actionList == null)
        return;
      for (int index = 0; index < actionList.Count; ++index)
      {
        InvokableAction invokableAction = actionList[index] as InvokableAction;
        if (invokableAction.IsAction(action))
        {
          GenericObjectPool.Return<InvokableAction>(invokableAction);
          actionList.RemoveAt(index);
          break;
        }
      }
      EventHandler.CheckForEventRemoval(obj, eventName, actionList);
    }

    public static void UnregisterEvent<T1>(string eventName, Action<T1> action)
    {
      List<InvokableActionBase> actionList = EventHandler.GetActionList(eventName);
      if (actionList == null)
        return;
      for (int index = 0; index < actionList.Count; ++index)
      {
        InvokableAction<T1> invokableAction = actionList[index] as InvokableAction<T1>;
        if (invokableAction.IsAction(action))
        {
          GenericObjectPool.Return<InvokableAction<T1>>(invokableAction);
          actionList.RemoveAt(index);
          break;
        }
      }
      EventHandler.CheckForEventRemoval(eventName, actionList);
    }

    public static void UnregisterEvent<T1>(object obj, string eventName, Action<T1> action)
    {
      List<InvokableActionBase> actionList = EventHandler.GetActionList(obj, eventName);
      if (actionList == null)
        return;
      for (int index = 0; index < actionList.Count; ++index)
      {
        InvokableAction<T1> invokableAction = actionList[index] as InvokableAction<T1>;
        if (invokableAction.IsAction(action))
        {
          GenericObjectPool.Return<InvokableAction<T1>>(invokableAction);
          actionList.RemoveAt(index);
          break;
        }
      }
      EventHandler.CheckForEventRemoval(obj, eventName, actionList);
    }

    public static void UnregisterEvent<T1, T2>(string eventName, Action<T1, T2> action)
    {
      List<InvokableActionBase> actionList = EventHandler.GetActionList(eventName);
      if (actionList == null)
        return;
      for (int index = 0; index < actionList.Count; ++index)
      {
        InvokableAction<T1, T2> invokableAction = actionList[index] as InvokableAction<T1, T2>;
        if (invokableAction.IsAction(action))
        {
          GenericObjectPool.Return<InvokableAction<T1, T2>>(invokableAction);
          actionList.RemoveAt(index);
          break;
        }
      }
      EventHandler.CheckForEventRemoval(eventName, actionList);
    }

    public static void UnregisterEvent<T1, T2>(object obj, string eventName, Action<T1, T2> action)
    {
      List<InvokableActionBase> actionList = EventHandler.GetActionList(obj, eventName);
      if (actionList == null)
        return;
      for (int index = 0; index < actionList.Count; ++index)
      {
        InvokableAction<T1, T2> invokableAction = actionList[index] as InvokableAction<T1, T2>;
        if (invokableAction.IsAction(action))
        {
          GenericObjectPool.Return<InvokableAction<T1, T2>>(invokableAction);
          actionList.RemoveAt(index);
          break;
        }
      }
      EventHandler.CheckForEventRemoval(obj, eventName, actionList);
    }

    public static void UnregisterEvent<T1, T2, T3>(string eventName, Action<T1, T2, T3> action)
    {
      List<InvokableActionBase> actionList = EventHandler.GetActionList(eventName);
      if (actionList == null)
        return;
      for (int index = 0; index < actionList.Count; ++index)
      {
        InvokableAction<T1, T2, T3> invokableAction = actionList[index] as InvokableAction<T1, T2, T3>;
        if (invokableAction.IsAction(action))
        {
          GenericObjectPool.Return<InvokableAction<T1, T2, T3>>(invokableAction);
          actionList.RemoveAt(index);
          break;
        }
      }
      EventHandler.CheckForEventRemoval(eventName, actionList);
    }

    public static void UnregisterEvent<T1, T2, T3>(
      object obj,
      string eventName,
      Action<T1, T2, T3> action)
    {
      List<InvokableActionBase> actionList = EventHandler.GetActionList(obj, eventName);
      if (actionList == null)
        return;
      for (int index = 0; index < actionList.Count; ++index)
      {
        InvokableAction<T1, T2, T3> invokableAction = actionList[index] as InvokableAction<T1, T2, T3>;
        if (invokableAction.IsAction(action))
        {
          GenericObjectPool.Return<InvokableAction<T1, T2, T3>>(invokableAction);
          actionList.RemoveAt(index);
          break;
        }
      }
      EventHandler.CheckForEventRemoval(obj, eventName, actionList);
    }

    public static void UnregisterEvent<T1, T2, T3, T4>(
      string eventName,
      Action<T1, T2, T3, T4> action)
    {
      List<InvokableActionBase> actionList = EventHandler.GetActionList(eventName);
      if (actionList == null)
        return;
      for (int index = 0; index < actionList.Count; ++index)
      {
        InvokableAction<T1, T2, T3, T4> invokableAction = actionList[index] as InvokableAction<T1, T2, T3, T4>;
        if (invokableAction.IsAction(action))
        {
          GenericObjectPool.Return<InvokableAction<T1, T2, T3, T4>>(invokableAction);
          actionList.RemoveAt(index);
          break;
        }
      }
      EventHandler.CheckForEventRemoval(eventName, actionList);
    }

    public static void UnregisterEvent<T1, T2, T3, T4>(
      object obj,
      string eventName,
      Action<T1, T2, T3, T4> action)
    {
      List<InvokableActionBase> actionList = EventHandler.GetActionList(obj, eventName);
      if (actionList == null)
        return;
      for (int index = 0; index < actionList.Count; ++index)
      {
        InvokableAction<T1, T2, T3, T4> invokableAction = actionList[index] as InvokableAction<T1, T2, T3, T4>;
        if (invokableAction.IsAction(action))
        {
          GenericObjectPool.Return<InvokableAction<T1, T2, T3, T4>>(invokableAction);
          actionList.RemoveAt(index);
          break;
        }
      }
      EventHandler.CheckForEventRemoval(obj, eventName, actionList);
    }

    public static void UnregisterEvent<T1, T2, T3, T4, T5>(
      string eventName,
      Action<T1, T2, T3, T4, T5> action)
    {
      List<InvokableActionBase> actionList = EventHandler.GetActionList(eventName);
      if (actionList == null)
        return;
      for (int index = 0; index < actionList.Count; ++index)
      {
        InvokableAction<T1, T2, T3, T4, T5> invokableAction = actionList[index] as InvokableAction<T1, T2, T3, T4, T5>;
        if (invokableAction.IsAction(action))
        {
          GenericObjectPool.Return<InvokableAction<T1, T2, T3, T4, T5>>(invokableAction);
          actionList.RemoveAt(index);
          break;
        }
      }
      EventHandler.CheckForEventRemoval(eventName, actionList);
    }

    public static void UnregisterEvent<T1, T2, T3, T4, T5>(
      object obj,
      string eventName,
      Action<T1, T2, T3, T4, T5> action)
    {
      List<InvokableActionBase> actionList = EventHandler.GetActionList(obj, eventName);
      if (actionList == null)
        return;
      for (int index = 0; index < actionList.Count; ++index)
      {
        InvokableAction<T1, T2, T3, T4, T5> invokableAction = actionList[index] as InvokableAction<T1, T2, T3, T4, T5>;
        if (invokableAction.IsAction(action))
        {
          GenericObjectPool.Return<InvokableAction<T1, T2, T3, T4, T5>>(invokableAction);
          actionList.RemoveAt(index);
          break;
        }
      }
      EventHandler.CheckForEventRemoval(obj, eventName, actionList);
    }

    public static void UnregisterEvent<T1, T2, T3, T4, T5, T6>(
      string eventName,
      Action<T1, T2, T3, T4, T5, T6> action)
    {
      List<InvokableActionBase> actionList = EventHandler.GetActionList(eventName);
      if (actionList == null)
        return;
      for (int index = 0; index < actionList.Count; ++index)
      {
        InvokableAction<T1, T2, T3, T4, T5, T6> invokableAction = actionList[index] as InvokableAction<T1, T2, T3, T4, T5, T6>;
        if (invokableAction.IsAction(action))
        {
          GenericObjectPool.Return<InvokableAction<T1, T2, T3, T4, T5, T6>>(invokableAction);
          actionList.RemoveAt(index);
          break;
        }
      }
      EventHandler.CheckForEventRemoval(eventName, actionList);
    }

    public static void UnregisterEvent<T1, T2, T3, T4, T5, T6>(
      object obj,
      string eventName,
      Action<T1, T2, T3, T4, T5, T6> action)
    {
      List<InvokableActionBase> actionList = EventHandler.GetActionList(obj, eventName);
      if (actionList == null)
        return;
      for (int index = 0; index < actionList.Count; ++index)
      {
        InvokableAction<T1, T2, T3, T4, T5, T6> invokableAction = actionList[index] as InvokableAction<T1, T2, T3, T4, T5, T6>;
        if (invokableAction.IsAction(action))
        {
          GenericObjectPool.Return<InvokableAction<T1, T2, T3, T4, T5, T6>>(invokableAction);
          actionList.RemoveAt(index);
          break;
        }
      }
      EventHandler.CheckForEventRemoval(obj, eventName, actionList);
    }

    public static void RegisterUnregisterEvent(bool register, string eventName, Action action)
    {
      if (register)
        EventHandler.RegisterEvent(eventName, action);
      else
        EventHandler.UnregisterEvent(eventName, action);
    }

    public static void RegisterUnregisterEvent(
      bool register,
      object obj,
      string eventName,
      Action action)
    {
      if (register)
        EventHandler.RegisterEvent(obj, eventName, action);
      else
        EventHandler.UnregisterEvent(obj, eventName, action);
    }

    public static void RegisterUnregisterEvent<T1>(
      bool register,
      string eventName,
      Action<T1> action)
    {
      if (register)
        EventHandler.RegisterEvent<T1>(eventName, action);
      else
        EventHandler.UnregisterEvent<T1>(eventName, action);
    }

    public static void RegisterUnregisterEvent<T1>(
      bool register,
      object obj,
      string eventName,
      Action<T1> action)
    {
      if (register)
        EventHandler.RegisterEvent<T1>(obj, eventName, action);
      else
        EventHandler.UnregisterEvent<T1>(obj, eventName, action);
    }

    public static void RegisterUnregisterEvent<T1, T2>(
      bool register,
      string eventName,
      Action<T1, T2> action)
    {
      if (register)
        EventHandler.RegisterEvent<T1, T2>(eventName, action);
      else
        EventHandler.UnregisterEvent<T1, T2>(eventName, action);
    }

    public static void RegisterUnregisterEvent<T1, T2>(
      bool register,
      object obj,
      string eventName,
      Action<T1, T2> action)
    {
      if (register)
        EventHandler.RegisterEvent<T1, T2>(obj, eventName, action);
      else
        EventHandler.UnregisterEvent<T1, T2>(obj, eventName, action);
    }

    public static void RegisterUnregisterEvent<T1, T2, T3>(
      bool register,
      string eventName,
      Action<T1, T2, T3> action)
    {
      if (register)
        EventHandler.RegisterEvent<T1, T2, T3>(eventName, action);
      else
        EventHandler.UnregisterEvent<T1, T2, T3>(eventName, action);
    }

    public static void RegisterUnregisterEvent<T1, T2, T3>(
      bool register,
      object obj,
      string eventName,
      Action<T1, T2, T3> action)
    {
      if (register)
        EventHandler.RegisterEvent<T1, T2, T3>(obj, eventName, action);
      else
        EventHandler.UnregisterEvent<T1, T2, T3>(obj, eventName, action);
    }

    public static void RegisterUnregisterEvent<T1, T2, T3, T4>(
      bool register,
      string eventName,
      Action<T1, T2, T3, T4> action)
    {
      if (register)
        EventHandler.RegisterEvent<T1, T2, T3, T4>(eventName, action);
      else
        EventHandler.UnregisterEvent<T1, T2, T3, T4>(eventName, action);
    }

    public static void RegisterUnregisterEvent<T1, T2, T3, T4>(
      bool register,
      object obj,
      string eventName,
      Action<T1, T2, T3, T4> action)
    {
      if (register)
        EventHandler.RegisterEvent<T1, T2, T3, T4>(obj, eventName, action);
      else
        EventHandler.UnregisterEvent<T1, T2, T3, T4>(obj, eventName, action);
    }

    public static void RegisterUnregisterEvent<T1, T2, T3, T4, T5>(
      bool register,
      string eventName,
      Action<T1, T2, T3, T4, T5> action)
    {
      if (register)
        EventHandler.RegisterEvent<T1, T2, T3, T4, T5>(eventName, action);
      else
        EventHandler.UnregisterEvent<T1, T2, T3, T4, T5>(eventName, action);
    }

    public static void RegisterUnregisterEvent<T1, T2, T3, T4, T5>(
      bool register,
      object obj,
      string eventName,
      Action<T1, T2, T3, T4, T5> action)
    {
      if (register)
        EventHandler.RegisterEvent<T1, T2, T3, T4, T5>(obj, eventName, action);
      else
        EventHandler.UnregisterEvent<T1, T2, T3, T4, T5>(obj, eventName, action);
    }

    public static void RegisterUnregisterEvent<T1, T2, T3, T4, T5, T6>(
      bool register,
      string eventName,
      Action<T1, T2, T3, T4, T5, T6> action)
    {
      if (register)
        EventHandler.RegisterEvent<T1, T2, T3, T4, T5, T6>(eventName, action);
      else
        EventHandler.UnregisterEvent<T1, T2, T3, T4, T5, T6>(eventName, action);
    }

    public static void RegisterUnregisterEvent<T1, T2, T3, T4, T5, T6>(
      bool register,
      object obj,
      string eventName,
      Action<T1, T2, T3, T4, T5, T6> action)
    {
      if (register)
        EventHandler.RegisterEvent<T1, T2, T3, T4, T5, T6>(obj, eventName, action);
      else
        EventHandler.UnregisterEvent<T1, T2, T3, T4, T5, T6>(obj, eventName, action);
    }

 
    private void OnDestroy() => this.ClearTable();

    private void ClearTable() => EventHandler.s_EventTable.Clear();
 
  }
}