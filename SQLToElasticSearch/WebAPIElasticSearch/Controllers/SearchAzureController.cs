using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIElasticSearch.IServices;

namespace WebAPIElasticSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class SearchAzureController : ControllerBase
    {
        private readonly IElasticServiceClient _elasticServiceClient;

        private readonly ISearchIndexClient _searchIndexClient;

        static string searchServiceName = "azuresearchservice11"; // ADD your Azure Search Service name
        static string adminApiKey = "5CA8C623E00D872D57F169CF262437DA"; // ADD your admin API key here
        static string queryApiKey = "62F7DCB1AFA88D7B8C6A122DDC65A78C";  // ADD you query APU key here
        static string indexName = "customers";
        ISearchIndexClient indexClient;

        public SearchAzureController(IElasticServiceClient elasticServiceClient)
        {
            _elasticServiceClient = elasticServiceClient;

            SearchServiceClient serviceClient = CreateSearchServiceClient(searchServiceName, adminApiKey);

             indexClient = serviceClient.Indexes.GetClient(indexName);
        }
        [HttpPost]
        [EnableCors("MyPolicy")]
        public async Task<ActionResult> AutoCompleteText(string searchText)
        {
            var result = await _elasticServiceClient.RunQueries(indexClient, searchText);

            return StatusCode(200, result);
        }

        private SearchServiceClient CreateSearchServiceClient(string searchServiceName, string adminApiKey)
        {
            SearchCredentials creds = new SearchCredentials(adminApiKey);
            SearchServiceClient serviceClient = new SearchServiceClient(searchServiceName, creds);
            return serviceClient;
        }
    }
}
