using DataAccess.Models;
using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using MVCCoreClient.IServices;
using MVCCoreClient.Models;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCCoreClient.Services
{
    public class ElasticServiceClient : IElasticServiceClient
    {
        private IConfiguration _iConfig;
        string INDEX_NAME = "productcatalog";
        public ElasticServiceClient(IConfiguration iConfig)
        {
            _iConfig = iConfig;
        }

        public ElasticClient ElasticClient()
        {
            ConnectionSettings connectionSettings;
            ElasticClient elasticClient = new Nest.ElasticClient();

            try
            {
                string connectionString = _iConfig.GetSection("ConnectionStrings").GetSection("EsConnection").Value;

                StaticConnectionPool connectionPool;

                //Multiple node for fail over (cluster addresses)
                var nodes = new Uri[] { new Uri(connectionString) };

                connectionPool = new StaticConnectionPool(nodes);
                connectionSettings = new ConnectionSettings(connectionPool);
                elasticClient = new ElasticClient(connectionSettings);
            }
            catch(Exception ex)
            {
            }
    
            return elasticClient;
        }

        public async Task<List<CustomerDto>> GetAutocompleteCustomers(string searchText)
        {
            List<CustomerDto> custometList = new List<CustomerDto>();

            try
            {
                var elasticClient = ElasticClient();

                var searchResponse = await elasticClient.SearchAsync<Customer>(s => s
                                        .Index("customer")
                                        .Query(q => q.MatchAll()));
             var   elacustometList = searchResponse.Documents.ToList();

               custometList = elacustometList.Where(x => x.Name.Contains(searchText) || x.Address.Contains(searchText))
                .Select(x => new CustomerDto
                {
                    Name = x.Name,
                    Address = x.Address
                })
                .OrderBy(x => x.Name)
                .ToList();
            }
            catch(Exception ex)
            {
            }

            return custometList;
        }
    }
}
