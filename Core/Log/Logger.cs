namespace WeCraft.Core
{
    public class Logger
    {
        protected string m_Prefix;
        public string Prefix
        {
            get { return m_Prefix;}
            set { m_Prefix = value; }
        }

        protected internal Logger(string prefix="Core")
        {
            m_Prefix = prefix;
        }
        
        public virtual void Info(string msg)
        {
            LogManager.Log(LogType.Info,m_Prefix,msg);
        }

        public virtual void Error(string msg)
        {
            LogManager.Log(LogType.Error,m_Prefix,msg);
        }

        public virtual void Debug(string msg)
        {
            LogManager.Log(LogType.Debug,m_Prefix,msg);
        }

        public virtual void Warn(string msg)
        {
            LogManager.Log(LogType.Warning,m_Prefix,msg);
        }
    }
}