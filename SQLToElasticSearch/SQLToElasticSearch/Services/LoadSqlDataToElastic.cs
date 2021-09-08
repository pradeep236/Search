using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using Confluent.Kafka;
using DataAccess.Repository;
using Microsoft.Azure.Search;
using Microsoft.Extensions.Hosting;
using SQLToElasticSearch.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SQLToElasticSearch.Services
{
    public class LoadSqlDataToElastic : BackgroundService
    {
        private readonly IElasticSearchService _elasticSearchService;
        
        private readonly ISqlRepository _sqlRepository;
        private readonly ConsumerConfig _consumerConfig;

        private readonly IElasticSearchCloud _elasticSearchCloud;
        static string searchServiceName = "azuresearchservice11"; // ADD your Azure Search Service name
        static string adminApiKey = "5CA8C623E00D872D57F169CF262437DA"; // ADD your admin API key here
        static string queryApiKey = "62F7DCB1AFA88D7B8C6A122DDC65A78C";  // ADD you query APU key here
        static string indexName = "customers";
        SearchServiceClient serviceClient;
        ISearchIndexClient indexClient;

        public LoadSqlDataToElastic(IElasticSearchService elasticSearchService, ISqlRepository sqlRepository,
            ConsumerConfig consumerConfig, IElasticSearchCloud elasticSearchCloud)
        {
            _elasticSearchService = elasticSearchService;
            _sqlRepository = sqlRepository;
            _consumerConfig = consumerConfig;
            _elasticSearchCloud = elasticSearchCloud;

             serviceClient = CreateSearchServiceClient(searchServiceName, adminApiKey);

             indexClient = serviceClient.Indexes.GetClient(indexName);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                string kafkaData = string.Empty;

                try
                {
                    var customers = await _sqlRepository.GetCustomersFromSql();
                   // var address = await _sqlRepository.GetAddressFromSql();

                    if (customers.Any() && customers.Count > 0)
                    {
                       // bool res = await _elasticSearchService.SaveCustomersToElasticsearch(customers, "customer").ConfigureAwait(false);
                        await _elasticSearchCloud.DeleteIndexIfExists(indexName, serviceClient);
                        await _elasticSearchCloud.CreateIndex(indexName, serviceClient);
                        await _elasticSearchCloud.UploadDocuments(indexClient, customers);

                        //if (res == true) Consumer.Commit(consumeResult);
                    }

                    //  List<Address> addressesList = JsonConvert.DeserializeObject<List<Address>>(kafkaData);
                    // if (address.Any() && address.Count > 0)
                    //{
                    // bool res = await _elasticSearchService.SaveAddressToElasticsearch(address).ConfigureAwait(false);
                    //if (res == true) Consumer.Commit(consumeResult);
                    // }
                    // }
                }

                catch (Exception ex)
                {

                }
                //var consumerHelper = new ConsumerWrapper(_consumerConfig, Constants.SQLTOELASTIC);

                //Consumer<string, string> Consumer = consumerHelper.ReadObjectData();

                //try
                //{
                //    ConsumeResult<string, string> consumeResult = Consumer.Consume();

                //    kafkaData = consumeResult.Value;
                //}
                //catch (Exception ex)
                //{

                //}

                //    if (!String.IsNullOrEmpty(kafkaData))
                // {
                //  List<Customer> customerList = JsonConvert.DeserializeObject<List<Customer>>(kafkaData);
            }
        }

        private SearchServiceClient CreateSearchServiceClient(string searchServiceName, string adminApiKey)
        {
            SearchCredentials creds = new SearchCredentials(adminApiKey);
            SearchServiceClient serviceClient = new SearchServiceClient(searchServiceName, creds);
            return serviceClient;
        }
    }
}
