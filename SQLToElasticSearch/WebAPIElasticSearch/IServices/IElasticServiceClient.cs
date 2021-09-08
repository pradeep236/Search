using Azure.Search.Documents;
using Microsoft.Azure.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIElasticSearch.Dto;

namespace WebAPIElasticSearch.IServices
{
  public  interface IElasticServiceClient
    {
        // Task<List<CustomerDto>> RunQueries(string searchTex);

        Task<List<CustomerModel>> RunQueries(ISearchIndexClient indexClient,string searchText);
    }
}
