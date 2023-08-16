namespace WeCraft.Core
{
    public interface ILogger
    {
        public void Info(string msg);

        public void Error(string msg);

        public  void Debug(string msg);

        public  void Warn(string msg);

        public void Error(System.Exception msg);
    }
}