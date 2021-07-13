using log4net;
using log4net.Config;
using LoggerMicroService.Models;
using System.IO;
using System.Reflection;

namespace LoggerMicroService.Helpers
{
    /// <summary>
    /// Logger class
    /// </summary>
    public class Logger : ILogger
    {
        private readonly ILog _logger = null;

        /// <summary>
        /// Constructor for loading Log4Net
        /// </summary>
        public Logger():this(LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType))
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }

        /// <summary>
        /// Constructor for <see cref="Logger"/>
        /// </summary>
        /// <param name="logger"><see cref="ILog"/></param>
        public Logger(ILog logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Logs message used for debugging purpose
        /// </summary>
        /// <param name="message">Log text</param>
        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        /// <summary>
        /// Logs message used as Information
        /// </summary>
        /// <param name="message">Log text</param>
        public void Info(string message)
        {
            _logger.Info(message);
        }

        /// <summary>
        /// Logs Error message
        /// </summary>
        /// <param name="message">Log text</param>
        public void Error(string message)
        {
            _logger.Error(message);
        }

        /// <summary>
        /// Logs message used as Warning
        /// </summary>
        /// <param name="message">Log text</param>
        public void Warn(string message)
        {
            _logger.Warn(message);
        }
    }
}
