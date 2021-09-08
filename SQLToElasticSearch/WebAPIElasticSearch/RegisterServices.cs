using Microsoft.Azure.Search;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIElasticSearch.IServices;
using WebAPIElasticSearch.Services;

namespace WebAPIElasticSearch
{
    public static class RegisterServices
    {
        /// <summary>
        /// RegisterConfigurationServices
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void RegisterConfigurationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var KafkaServer = Environment.GetEnvironmentVariable("KafkaServer");

            services.AddSingleton<IElasticServiceClient, ElasticServiceClient>();
        }
    }
}
