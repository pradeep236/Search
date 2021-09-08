using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLToElasticSearch.IServices
{
   public interface IElasticSearchService
    {
        Task<bool> SaveAddressToElasticsearch(List<Address> addresses);
        Task<bool> SaveCustomersToElasticsearch(List<Customer> customers, string indexName);
    }
}
