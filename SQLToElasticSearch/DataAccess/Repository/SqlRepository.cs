using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class SqlRepository : ISqlRepository
    {
        public SqlRepository()
        {
        }

        public async Task<List<Address>> GetAddressFromSql()
        {
            List<Address> addresses = null;

            try
            {
                using (var _context = new SampleDbContext())
                {
                    addresses = new List<Address>();

                    int length = _context.Addresses.Where(t => t.CreateTs.Equals(DateTime.Now) || t.UpdateTs.Equals(DateTime.Now)).Count();

                    if (length > 0)
                    {
                        addresses = await _context.Addresses.Where(t => t.CreateTs.Equals(DateTime.Now) || t.UpdateTs.Equals(DateTime.Now)).ToListAsync();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return addresses;
        }

        public async Task<List<Customer>> GetCustomersFromSql()
        {
            List<Customer> customers = null;

            try
            {
                using (var _context = new SampleDbContext())
                {
                    customers = new List<Customer>();

                    //int length = _context.Customers.Where(t => t.CreateTs.Equals(DateTime.Now) || t.UpdateTs.Equals(DateTime.Now)).Count();
                    int length = _context.Customers.Count();

                    if (length > 0)
                    {
                        customers = await _context.Customers.ToListAsync();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return customers;
        }
    }
}
