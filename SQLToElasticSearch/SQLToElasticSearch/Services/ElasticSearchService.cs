using DataAccess.Models;
using ElasticsearchCRUD;
using Nest;
using SQLToElasticSearch.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLToElasticSearch.Services
{
    public class ElasticSearchService : IElasticSearchService
    {
		private readonly IElasticsearchMappingResolver _elasticsearchMappingResolver;

		private readonly IElasticServiceClient _elasticServiceClient;

		public ElasticSearchService(IElasticsearchMappingResolver elasticsearchMappingResolver, IElasticServiceClient elasticServiceClient)
		{
			_elasticsearchMappingResolver = elasticsearchMappingResolver;
			_elasticServiceClient = elasticServiceClient;
		}

		public async Task<bool> SaveAddressToElasticsearch(List<Address> addresses)
		{
			bool iSresult = false;

			try
			{
				using (var ElasticsearchContext = new ElasticsearchContext("http://51.103.129.151:32894", _elasticsearchMappingResolver))
				{
					foreach (var item in addresses)
					{
						ElasticsearchContext.AddUpdateDocument(item, item.AddressId);
					}

					await ElasticsearchContext.SaveChangesAsync();
					iSresult = true;
				}
			}
			catch (Exception ex)
			{
			}

			return iSresult;
		}

		public async Task<bool> SaveCustomersToElasticsearch(List<Customer> customers, string indexName)
		{
			bool iSresult = false;

			var elasticClient = _elasticServiceClient.ElasticClient();

			try
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

				//var searchResponse = elasticClient.Search<Customer>(s => s
				//				.Index(indexName)
				//				.Query(q => q.MatchAll()
				//					   )
				//				 );
			
			}
			catch (Exception ex)
			{
			}
			return iSresult;
		}
	}
}
