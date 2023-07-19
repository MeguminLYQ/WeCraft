namespace WeCraft.Core.Plugin
{
    public interface IPluginManager
    {
        /// <summary>
        /// 所有插件
        /// </summary>
        public PluginBase[] Plugins { get;}
         
    }
}