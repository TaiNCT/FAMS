using NLog;
using SyllabusManagementAPI.Contracts;
using ILogger = NLog.ILogger;

namespace SyllabusManagementAPI.LoggerService
{
    /// <summary>
    /// Represents a logger manager that provides logging functionality.
    /// </summary>
    public class LoggerManager : ILoggerManager
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Logs a debug message.
        /// </summary>
        /// <param name="message">The debug message to be logged.</param>
        public void LogDebug(string message) => logger.Debug(message);

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">The error message to be logged.</param>
        public void LogError(string message) => logger.Error(message);

        /// <summary>
        /// Logs an information message.
        /// </summary>
        /// <param name="message">The information message to be logged.</param>
        public void LogInfo(string message) => logger.Info(message);

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="message">The warning message to be logged.</param>
        public void LogWarn(string message) => logger.Warn(message);
    }
}
