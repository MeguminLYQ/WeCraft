using System;

namespace WeCraft.Core.EventHandler
{
    public abstract class InvokableActionBase
    {
        
    }
    internal class InvokableAction : InvokableActionBase
    {
        private event Action m_Action;

        public void Initialize(Action action) => this.m_Action = action;

        public void Invoke() => this.m_Action();

        public bool IsAction(Action action) => this.m_Action == action;
    }
    internal class InvokableAction<T1> : InvokableActionBase
    {
        private event Action<T1> m_Action;

        public void Initialize(Action<T1> action) => this.m_Action = action;

        public void Invoke(T1 arg1) => this.m_Action(arg1);

        public bool IsAction(Action<T1> action) => this.m_Action == action;
    }
    internal class InvokableAction<T1, T2> : InvokableActionBase
    {
        private event Action<T1, T2> m_Action;

        public void Initialize(Action<T1, T2> action) => this.m_Action = action;

        public void Invoke(T1 arg1, T2 arg2) => this.m_Action(arg1, arg2);

        public bool IsAction(Action<T1, T2> action) => this.m_Action == action;
    }
    internal class InvokableAction<T1, T2, T3> : InvokableActionBase
    {
        private event Action<T1, T2, T3> m_Action;

        public void Initialize(Action<T1, T2, T3> action) => this.m_Action = action;

        public void Invoke(T1 arg1, T2 arg2, T3 arg3) => this.m_Action(arg1, arg2, arg3);

        public bool IsAction(Action<T1, T2, T3> action) => this.m_Action == action;
    }
    internal class InvokableAction<T1, T2, T3, T4> : InvokableActionBase
    {
        private event Action<T1, T2, T3, T4> m_Action;

        public void Initialize(Action<T1, T2, T3, T4> action) => this.m_Action = action;

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4) => this.m_Action(arg1, arg2, arg3, arg4);

        public bool IsAction(Action<T1, T2, T3, T4> action) => this.m_Action == action;
    }
    internal class InvokableAction<T1, T2, T3, T4, T5> : InvokableActionBase
    {
        private event Action<T1, T2, T3, T4, T5> m_Action;

        public void Initialize(Action<T1, T2, T3, T4, T5> action) => this.m_Action = action;

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) => this.m_Action(arg1, arg2, arg3, arg4, arg5);

        public bool IsAction(Action<T1, T2, T3, T4, T5> action) => this.m_Action == action;
    }
    internal class InvokableAction<T1, T2, T3, T4, T5, T6> : InvokableActionBase
    {
        private event Action<T1, T2, T3, T4, T5, T6> m_Action;

        public void Initialize(Action<T1, T2, T3, T4, T5, T6> action) => this.m_Action = action;

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6) => this.m_Action(arg1, arg2, arg3, arg4, arg5, arg6);

        public bool IsAction(Action<T1, T2, T3, T4, T5, T6> action) => this.m_Action == action;
    }
}