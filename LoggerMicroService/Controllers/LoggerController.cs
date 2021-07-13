using LoggerMicroService.Helpers;
using LoggerMicroService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggerMicroService.Controllers
{
    /// <summary>
    /// Logger Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LoggerController : ControllerBase
    {
        private IServiceHelper _serviceHelper;
        private readonly ILogger _logger;
        private readonly IMessageLogger _messageLogger;

        /// <summary>
        /// Constructor for <see cref="LoggerController"/> with DI
        /// </summary>
        /// <param name="serviceHelper"><see cref="IServiceHelper"/></param>
        /// <param name="logger"><see cref="ILogger"/></param>
        /// <param name="messageLogger"><see cref="IMessageLogger"/></param>
        public LoggerController(IServiceHelper serviceHelper,
            ILogger logger,
            IMessageLogger messageLogger)
        {
            _serviceHelper = serviceHelper;
            _logger = logger;
            _messageLogger = messageLogger;
        }

        /// <summary>
        /// Process transaction by calling external service 
        /// and logging response from external service
        /// </summary>
        /// <returns><see cref="ServiceResponse"/></returns>
        [HttpPost]
        public async Task<ActionResult> ProcessTransaction()
        {
            try
            {
                ServiceResponse serviceResponse = await _serviceHelper.GetAsync();

                bool isValidResponse = serviceResponse != null && !string.IsNullOrEmpty(serviceResponse.Message);

                if (isValidResponse)
                {
                    var message = serviceResponse.Message;

                    if (message.Length > 255)
                    {
                        _logger.Warn($"Truncating Message to 255 charcters for MessageId : {serviceResponse.ID}");
                        serviceResponse.Message = message.Substring(0, 255);
                    }

                    _messageLogger.MessageLogger_Info(serviceResponse);
                    return Ok(serviceResponse);
                }

                if (serviceResponse == null)
                    _logger.Error("Invalid Response form External Service - Null Response");

                if (string.IsNullOrEmpty(serviceResponse.Message))
                    _logger.Error($"Invalid Response form External Service - Message is null or empty for Message ID {serviceResponse.ID}");

                return UnprocessableEntity("Unable to Process Request at this time. Please contact System Admin!!");
            }
            catch(Exception e)
            {
                _logger.Error($"Unable to process Transaction due to exception : {e}");
                throw;
            }
        }
    }
}
