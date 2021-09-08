using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Azure.Search.Documents.Models;
using DataAccess.Models;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIElasticSearch.Dto;
using WebAPIElasticSearch.IServices;


namespace WebAPIElasticSearch.Services
{
    public class ElasticServiceClient : IElasticServiceClient
    {
        static string searchServiceName = "azuresearchservice11"; // ADD your Azure Search Service name
        static string adminApiKey = "5CA8C623E00D872D57F169CF262437DA"; // ADD your admin API key here
        static string queryApiKey = "62F7DCB1AFA88D7B8C6A122DDC65A78C";  // ADD you query APU key here
        static string indexName = "customers";

        public ElasticServiceClient()
        {
            SearchServiceClient serviceClient = CreateSearchServiceClient(searchServiceName, adminApiKey);

            ISearchIndexClient indexClient = serviceClient.Indexes.GetClient(indexName);
        }

        public SearchServiceClient CreateSearchServiceClient(string searchServiceName, string adminApiKey)
        {
            SearchCredentials creds = new SearchCredentials(adminApiKey);
            SearchServiceClient serviceClient = new SearchServiceClient(searchServiceName, creds);
            return serviceClient;
        }

        public async Task<List<CustomerModel>> RunQueries(ISearchIndexClient indexClient, string searchText)
        {
            List<CustomerModel> customerDtos = new List<CustomerModel>();

            try
            {
                SearchParameters parameters =

                      new SearchParameters()
                        {
                         Select = new[] { "NAME", "PhoneNumber", "Email", "Address" }
                        };

                SearchParameters parameters1 = new SearchParameters();

                DocumentSearchResult<CustomerModel> results = await indexClient.Documents.SearchAsync<CustomerModel>(searchText, parameters);

                DocumentSearchResult<CustomerModel> result1 = indexClient.Documents.Search<CustomerModel>(searchText, parameters);

                foreach (var result in results.Results)
                {
                    CustomerModel customerDto = new CustomerModel();
                    customerDto.NAME = result.Document.NAME;
                    customerDto.PhoneNumber = result.Document.PhoneNumber;
                    customerDto.Email = result.Document.Email;
                    customerDto.Address = result.Document.Address;
                    customerDtos.Add(customerDto);
                }
            }
            catch (Exception ex)
            {
            }
            return customerDtos;
        }
    }
}
