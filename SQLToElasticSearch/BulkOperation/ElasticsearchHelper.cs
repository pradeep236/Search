using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using Elasticsearch.Net;
using Nest;

namespace BulkOperation
{
    class ElasticsearchHelper
    {
        public static ElasticClient GetESClient()
        {
            ConnectionSettings connectionSettings;
            ElasticClient elasticClient;
            StaticConnectionPool connectionPool;
            var nodes = new Uri[] {
                new Uri("http://51.103.129.151:32894") //Provide ES cluster addresses)
            };
            connectionPool = new StaticConnectionPool(nodes);
            connectionSettings = new ConnectionSettings(connectionPool);
            elasticClient = new ElasticClient(connectionSettings);

            return elasticClient;
        }

        public static async Task CreateBulkDocument(ElasticClient elasticClient, string indexName, List<Customer> customers)
        {
            var bulkResponse = await elasticClient.BulkAsync(b => b
                                       .Index(indexName)
                                       .IndexMany(customers));


            if (bulkResponse.ApiCall.Success && bulkResponse.IsValid)
            {
                // success fully inserted..
                Console.WriteLine("Bulk Document Inserted.");
            }
            else
            {
                Console.WriteLine(bulkResponse.OriginalException.ToString());
            }
        }
    }
}
