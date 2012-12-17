using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csharp_github_api.Logging
{
    /// <summary>
    /// The default logger until one is set.
    /// </summary>
    /// <remarks>
    /// Taken from https://gist.github.com/3099122
    /// </remarks>
    public class NullLogger : ILog, ILog<NullLogger>
    {
        public void InitializeFor(string loggerName)
        {
        }

        public void Debug(string message, params object[] formatting)
        {
        }

        public void Debug(Func<string> message)
        {
        }

        public void Info(string message, params object[] formatting)
        {
        }

        public void Info(Func<string> message)
        {
        }

        public void Warn(string message, params object[] formatting)
        {
        }

        public void Warn(Func<string> message)
        {
        }

        public void Error(string message, params object[] formatting)
        {
        }

        public void Error(Func<string> message)
        {
        }

        public void Fatal(string message, params object[] formatting)
        {
        }

        public void Fatal(Func<string> message)
        {
        }
    }
}
