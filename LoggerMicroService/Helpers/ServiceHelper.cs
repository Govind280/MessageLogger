using LoggerMicroService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LoggerMicroService.Helpers
{
    /// <summary>
    /// ServiceHelper class
    /// </summary>
    public class ServiceHelper : IServiceHelper
    {
        private const string _baseUrl = "{External Service URL}";

        /// <summary>
        /// Calls external Service
        /// </summary>
        /// <returns><see cref="ServiceResponse"/></returns>
        public async Task<ServiceResponse> GetAsync()
        {
            using var client = new HttpClient();

            var htttpResponse = await client.GetAsync(_baseUrl);

            if(!htttpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrive data");
            }

            var content = await htttpResponse.Content.ReadAsStringAsync();
            var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse>(content);

            return serviceResponse;
        }
    }
}
