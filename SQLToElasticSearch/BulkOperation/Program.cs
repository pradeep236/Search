using DataAccess.Models;
using Nest;
using System;
using System.Collections.Generic;

namespace BulkOperation
{
    class Program
    {
        static void Main(string[] args)
        {
            string INDEX_NAME = "customers";
            //1. Get Product list from the database.

            var customers = GetAllCustomer();

            //2.Connect to Elastic Search.
            ElasticClient elasticClient = ElasticsearchHelper.GetESClient();

            if (customers != null && customers.Count > 0)
            {
                //2. Insert in to Elastic search
                ElasticsearchHelper.CreateBulkDocument(elasticClient, INDEX_NAME, customers).Wait();
            }
        }

        private static List<Customer> GetAllCustomer()
        {
            List<Customer> customers = new List<Customer>();

            using (var context= new SampleDbContext())
            {
                Customer customer = null;


                for (int i=1;i<1000;i++)
                {
                    customer = new Customer();

                    customer.Name = "TestName" + i;
                    customer.Address = "Bangalore" + i;
                    customer.Email = "test@gmail.com" + i;
                    customer.Phone = "123" + i;
                    customer.CreateUser = "user" + i;
                    customer.UpdateUser = "user" + i;
                    customer.CreateTs = DateTime.Now;
                    customer.UpdateTs = DateTime.Now;

                    customers.Add(customer);
                }
                context.Customers.AddRange(customers);
                context.SaveChanges();
            }

            return customers;
        }
    }
}
