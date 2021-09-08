using DataAccess.Models;
using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Nest;
using SQLToElasticSearch.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLToElasticSearch.Services
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
            string connectionString = _iConfig.GetSection("ConnectionStrings").GetSection("EsConnection").Value;
            ConnectionSettings connectionSettings;
            ElasticClient elasticClient;
            StaticConnectionPool connectionPool;

            //Multiple node for fail over (cluster addresses)
            var nodes = new Uri[] { new Uri(connectionString) };

            connectionPool = new StaticConnectionPool(nodes);
            connectionSettings = new ConnectionSettings(connectionPool);
            elasticClient = new ElasticClient(connectionSettings);
            return elasticClient;
        }

        public IEnumerable<Customer> GetAutocompleteSuggestions(string searchText)
        {
            ElasticClient elasticClient = ElasticClient();
            ISearchResponse<Customer> searchResponse = elasticClient.Search<Customer>(s => s
                                     .Index(INDEX_NAME)
                                     .Suggest(su => su
                                          .Completion("suggestedProducts", c => c
                                               .Field(f => f.Name)
                                               .Prefix(searchText)
                                               .Fuzzy(f => f
                                                   .Fuzziness(Fuzziness.Auto)
                                               )
                                               .Size(5))
                                             ));

            var suggests = from suggest in searchResponse.Suggest["suggestedProducts"]
                           from option in suggest.Options
                           select new Customer
                           {
                               Name = option.Source.Name,
                               Email = option.Source.Email,
                               Address = option.Source.Address
                           };

            return suggests;
        }
    }
}
