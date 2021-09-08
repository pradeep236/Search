using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVCCoreClient.IServices;
using MVCCoreClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCCoreClient
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