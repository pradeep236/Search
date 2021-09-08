using DataAccess.Models;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLToElasticSearch.IServices
{
  public  interface IElasticServiceClient
    {
        ElasticClient ElasticClient();
        IEnumerable<Customer> GetAutocompleteSuggestions(string searchText);
    }
}
