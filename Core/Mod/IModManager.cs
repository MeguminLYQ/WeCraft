namespace WeCraft.Core.Mod
{
    public interface IModManager
    {
        /// <summary>
        /// 所有插件
        /// </summary>
        public ModBase[] Mods { get;}

        public void CallPreInitialization();
        public void CallInitialization();
        public void CallPostInitialization();
    }
}