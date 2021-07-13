using log4net;
using log4net.Config;
using LoggerMicroService.Models;
using System.IO;
using System.Reflection;

namespace LoggerMicroService.Helpers
{
    /// <summary>
    /// MessageLogger class
    /// </summary>
    public class MessageLogger : IMessageLogger
    {
        private readonly ILog _messageLogger = LogManager.GetLogger("MessageLogger");

        /// <summary>
        /// Constructor for loading Log4Net
        /// </summary>
        public MessageLogger():this(LogManager.GetLogger("MessageLogger"))
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }

        /// <summary>
        /// Constructor for <see cref="MessageLogger"/>
        /// </summary>
        /// <param name="messageLogger"><see cref="ILog"/></param>
        public MessageLogger(ILog messageLogger)
        {
            _messageLogger = messageLogger;
        }

        /// <summary>
        /// Logs Service response to Log file
        /// </summary>
        /// <param name="serviceResponse"><see cref="ServiceResponse"/>/param>
        public void MessageLogger_Info(ServiceResponse serviceResponse)
        {            
            _messageLogger.Info($"Message Id : {serviceResponse.ID} | Message Date : {serviceResponse.MessageDate} |" +
                $"Message : {serviceResponse.Message}");
        }
    }
}
