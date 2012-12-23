using System;
using Logging = global::LoggingExtensions.Logging;
using VS = System.Diagnostics.Debug;

namespace csharp_github_api.IntegrationTests
{
    public struct Categories
    {
        public static string DEBUG = "Debug";
        public static string INFO = "Info";
        public static string WARN = "Warn";
        public static string ERROR = "Error";
        public static string FATAL = "Fatal";
    }
    public class DebugLogger : Logging.ILog, Logging.ILog<DebugLogger>
    {
        private static string _loggerName;

        private readonly Action<string, string, object[]> _writeWithFormatting
            = (category, message, objects) =>
                  {
                      var formatted = string.Format(message, objects);
                      var output = string.Format("{0} - {1}", _loggerName, formatted);
                      VS.WriteLine(output, category);
                  };

        private readonly Action<string, Func<string>> _write = (category, message) =>
                                                                   {
                                                                       var output = string.Format("{0} - {1}",
                                                                                                  _loggerName,
                                                                                                  message.Invoke());
                                                                       VS.WriteLine(output, category);
                                                                   };

        private static void WriteWithFormatting(string category, string message, params object[] formatting)
        {
            throw new NotImplementedException();
        }

        public void InitializeFor(string loggerName)
        {
            _loggerName = loggerName;
        }

        /// <summary>
        /// Debug level of the specified message. The other method is preferred since the execution is deferred.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="formatting">The formatting.</param>
        public void Debug(string message, params object[] formatting)
        {
            _writeWithFormatting(Categories.DEBUG, message, formatting);
        }

        /// <summary>
        /// Debug level of the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Debug(Func<string> message)
        {
            _write(Categories.DEBUG, message);
        }

        /// <summary>
        /// Info level of the specified message. The other method is preferred since the execution is deferred.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="formatting">The formatting.</param>
        public void Info(string message, params object[] formatting)
        {
            _writeWithFormatting(Categories.INFO, message, formatting);
        }

        /// <summary>
        /// Info level of the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Info(Func<string> message)
        {
            _write(Categories.INFO, message);
        }

        /// <summary>
        /// Warn level of the specified message. The other method is preferred since the execution is deferred.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="formatting">The formatting.</param>
        public void Warn(string message, params object[] formatting)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Warn level of the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Warn(Func<string> message)
        {
            _write(Categories.WARN, message);
        }

        /// <summary>
        /// Error level of the specified message. The other method is preferred since the execution is deferred.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="formatting">The formatting.</param>
        public void Error(string message, params object[] formatting)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Error level of the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Error(Func<string> message)
        {
            _write(Categories.ERROR, message);
        }

        /// <summary>
        /// Fatal level of the specified message. The other method is preferred since the execution is deferred.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="formatting">The formatting.</param>
        public void Fatal(string message, params object[] formatting)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Fatal level of the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Fatal(Func<string> message)
        {
            _write(Categories.FATAL, message);
        }
    }
}
