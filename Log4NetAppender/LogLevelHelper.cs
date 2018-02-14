using log4net.Core;

namespace Zidium
{
    internal static class LogLevelHelper
    {
        public static Api.LogLevel GetLogLevel(Level level)
        {
            if (level <= Level.Trace)
                return Api.LogLevel.Trace;

            if (level <= Level.Debug)
                return Api.LogLevel.Debug;

            if (level <= Level.Info)
                return Api.LogLevel.Info;

            if (level <= Level.Warn)
                return Api.LogLevel.Warning;

            if (level <= Level.Error)
                return Api.LogLevel.Error;

            return Api.LogLevel.Fatal;

        }
    }
}
