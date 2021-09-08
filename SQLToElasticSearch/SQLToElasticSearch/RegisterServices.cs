using Confluent.Kafka;
using DataAccess.Models;
using ElasticsearchCRUD;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SQLToElasticSearch.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using HostedServices = Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using SQLToElasticSearch.Services;
using DataAccess.Repository;
using SQLToElasticSearch.IServices;
using Microsoft.EntityFrameworkCore;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents;
using Azure;
using Microsoft.Azure.Search;

namespace SQLToElasticSearch
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

            services.AddSingleton<IHostedService, LoadSqlDataToElastic>();

            services.AddSingleton<IElasticsearchMappingResolver, ElasticsearchMappingResolver>();
            services.AddSingleton<IElasticSearchService, ElasticSearchService>();
            services.AddSingleton<IElasticServiceClient, ElasticServiceClient>();
            services.AddSingleton<IElasticSearchCloud, ElasticSearchCloud>();

            services.AddSingleton<ISqlRepository, SqlRepository>();

            var producerConfig = new ProducerConfig()
            {
                BootstrapServers = KafkaServer,
                RequestTimeoutMs = Constants.Request_Timeout,
                SessionTimeoutMs = Constants.Session_Timeout,
                SocketTimeoutMs = Constants.Socket_Timeout,
                MessageTimeoutMs = Constants.Message_Timeout
            };

            configuration.Bind(Constants.Producer, producerConfig);
            services.AddSingleton<ProducerConfig>(producerConfig);

            var consumerConfig = new ConsumerConfig()
            {
                BootstrapServers = KafkaServer,
                GroupId = Constants.GroupId,
                // EnableAutoCommit = Constants.EnableAutoCommit,
                StatisticsIntervalMs = Constants.StatisticsIntervalMs,
                SessionTimeoutMs = Constants.SessionTimeoutMs,
                // AutoOffsetReset= KafkaConstants.AutoOffsetReset,
                EnableAutoOffsetStore = Constants.EnableAutoOffsetStore,
                SaslMechanism = Constants.SaslMechanism
            };
            configuration.Bind(Constants.Consumer, consumerConfig);
            services.AddSingleton<ConsumerConfig>(consumerConfig);

            string connectionString = Environment.GetEnvironmentVariable("SqlConnection");
            services.AddDbContext<SampleDbContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Singleton);
        }
    }
}
