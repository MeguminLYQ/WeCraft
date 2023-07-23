namespace WeCraft.Core.Mod
{
    public abstract class ModBase
    {  
        public string Name { get; protected set; }
        public abstract void OnLoad();
        public abstract void OnEnable();
        public abstract void OnDisable();
    }
}