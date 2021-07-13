using LoggerMicroService.Models;

namespace LoggerMicroService.Helpers
{
    /// <summary>
    /// MessageLogger Interface
    /// </summary>
    public interface IMessageLogger
    {
        /// <summary>
        /// Logs Service response to Log file
        /// </summary>
        /// <param name="serviceResponse"><see cref="ServiceResponse"/>/param>
        void MessageLogger_Info(ServiceResponse serviceResponse);
    }
}
