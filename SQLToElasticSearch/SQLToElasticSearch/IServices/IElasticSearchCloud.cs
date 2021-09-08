using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using DataAccess.Models;
using Microsoft.Azure.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLToElasticSearch.IServices
{
   public interface IElasticSearchCloud
    {
        //Task<int> DeleteIndexIfExists( string indexName, SearchIndexClient adminClient);


        //Task<int> CreateIndex(string indexName, SearchIndexClient adminClient);

        // Task<int>  UploadDocuments(SearchClient ingesterClient, List<Customer> customers);

        Task<int> DeleteIndexIfExists(string indexName, SearchServiceClient serviceClient);

        Task<int> CreateIndex(string indexName, SearchServiceClient serviceClient);

        Task<int> UploadDocuments(ISearchIndexClient indexClient, List<Customer> customers);

    }
}
