using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
  public  interface ISqlRepository
    {
        Task<List<Customer>> GetCustomersFromSql();

        Task<List<Address>> GetAddressFromSql();
    }
}
