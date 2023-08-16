using System;
using System.Collections.Generic; 

namespace WeCraft.Core{


    public sealed class LogManager
    {
        private static readonly Dictionary<LogType, LogMethod> m_LogMethods = new Dictionary<LogType, LogMethod>();
        private static string m_TimestampFormat;
        private static bool m_IncludeTimestamps;

        private static bool IsDebugLoggingEnabled => m_LogMethods.ContainsKey(LogType.Debug);
        private static bool IsInfoLoggingEnabled => m_LogMethods.ContainsKey(LogType.Info);
        private static bool IsWarningLoggingEnabled => m_LogMethods.ContainsKey(LogType.Warning);
        private static bool IsErrorLoggingEnabled => m_LogMethods.ContainsKey(LogType.Error);
        private static string GetTimestamp(DateTime time) => time.ToString(m_TimestampFormat);

        public static void Initialize(LogMethod logMethod, bool includeTimestamps, string timestampFormat = "HH:mm:ss")
        {
            Initialize(logMethod,logMethod,logMethod,logMethod,includeTimestamps,timestampFormat);
        }
        
        public static void Initialize(LogMethod debugMethod,LogMethod infoMethod,LogMethod warningMethod,LogMethod errorMethod, bool includeTimestamps, string timestampFormat = "HH:mm:ss")
        {
            m_LogMethods.Clear();
            if(debugMethod!=null)
            {
                m_LogMethods.Add(LogType.Debug, debugMethod);
            }
            if(infoMethod!=null)
            {
                m_LogMethods.Add(LogType.Info, infoMethod);
            }
            if(warningMethod!=null)
            {
                m_LogMethods.Add(LogType.Warning, warningMethod);
            }
            if(errorMethod!=null)
            {
                m_LogMethods.Add(LogType.Error, errorMethod);
            }
            m_IncludeTimestamps = includeTimestamps;
            m_TimestampFormat = timestampFormat;
        }

        public static void Log(LogType logType, string logName, string message)
        {
            LogMethod logMethod;
            if (!m_LogMethods.TryGetValue(logType, out logMethod))
            {
                return;
            }

            if (m_IncludeTimestamps)
            {
                logMethod($"[{GetTimestamp(DateTime.Now)}] ({logName}): {message}");
            }
            else
            {
                logMethod($" ({logName}): {message}");
            }
        }
        public static void Log(LogType logType, string message)
        {            
            LogMethod logMethod;
            if (!m_LogMethods.TryGetValue(logType, out logMethod))
            {
                return;
            }

            if (m_IncludeTimestamps)
            {
                logMethod($"[{GetTimestamp(DateTime.Now)}] {message}");
            }
            else
            {
                logMethod($"{message}");
            }
        }

        public static Logger GetLogger(string name)
        {
            return new Logger(name);
        }


    }

    public delegate void LogMethod(string log);

    public enum LogType
    {
        /// <summary>Logs that are used for investigation during development.</summary>
        Debug = 0,
        /// <summary>Logs that provide general information about application flow.</summary>
        Info = 1,
        /// <summary>Logs that highlight abnormal or unexpected events in the application flow.</summary>
        Warning = 2,
        /// <summary>Logs that highlight problematic events in the application flow which will cause unexpected behavior if not planned for.</summary>
        Error = 3,
    }
}