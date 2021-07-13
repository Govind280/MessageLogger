using LoggerMicroService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggerMicroService.Helpers
{
    /// <summary>
    /// IServiceHelper Interface
    /// </summary>
    public interface IServiceHelper
    {
        /// <summary>
        /// Calls external service to get message
        /// </summary>
        /// <returns><see cref="ServiceResponse"/></returns>
        Task<ServiceResponse> GetAsync();
    }
}
