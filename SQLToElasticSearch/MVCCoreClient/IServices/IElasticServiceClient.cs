using DataAccess.Models;
using MVCCoreClient.Models;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCCoreClient.IServices
{
    public interface IElasticServiceClient
    {
        ElasticClient ElasticClient();
        Task<List<CustomerDto>> GetAutocompleteCustomers(string searchText);
    }
}
