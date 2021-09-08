using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Azure.Search.Documents.Models;
using DataAccess.Models;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using SQLToElasticSearch.Dto;
using SQLToElasticSearch.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SQLToElasticSearch.Services
{
    public class ElasticSearchCloud : IElasticSearchCloud
    {
        static string searchServiceName = "azuresearchservice11"; // ADD your Azure Search Service name
        static string adminApiKey = "5CA8C623E00D872D57F169CF262437DA"; // ADD your admin API key here
        static string queryApiKey = "62F7DCB1AFA88D7B8C6A122DDC65A78C";  // ADD you query APU key here
        static string indexName = "customers";

        public ElasticSearchCloud()
        {
            SearchServiceClient serviceClient = CreateSearchServiceClient(searchServiceName, adminApiKey);

            ISearchIndexClient indexClient = serviceClient.Indexes.GetClient(indexName);
        }

        public async Task<int> DeleteIndexIfExists(string indexName, SearchServiceClient serviceClient)
        {
            int result = 0;

            try
            {
                if (serviceClient.Indexes.Exists(indexName))
                {
                  await  serviceClient.Indexes.DeleteAsync(indexName);
                }
            }
            catch(Exception ex)
            {
            }
            return result;
        }

        public async Task<int> CreateIndex(string indexName, SearchServiceClient serviceClient)
        {
            int result = 0;

            try
            {
                var definition = new Microsoft.Azure.Search.Models.Index()
                {
                    Name = indexName,
                    Fields = Microsoft.Azure.Search.FieldBuilder.BuildForType<CustomerModel>()
                };
                await serviceClient.Indexes.CreateAsync(definition);
            }
            catch(Exception ex)
            {

            }

            return result;
        }



        public async Task<int> UploadDocuments(ISearchIndexClient indexClient, List<Customer> customers)
        {
            int result = 0;

            List<CustomerModel> customerCollections = new List<CustomerModel>();

            try
            {
                var actions = new IndexAction<CustomerModel>[customers.Count];

                int i = 0;
                foreach (var cust in customers)
                {
                    CustomerModel customer = new CustomerModel
                    {
                        ID = Convert.ToString(cust.Id),
                        NAME = cust.Name,
                        Address = cust.Address,
                        Email = cust.Email,
                        PhoneNumber = cust.Phone
                    };
                    customerCollections.Add(customer);
                    actions[i] = IndexAction.Upload(customer);

                    i++;
                }
                IndexBatch<CustomerModel> batch = IndexBatch.New(actions);
                await indexClient.Documents.IndexAsync(batch);
            }
            catch (Exception e)
            {
                // When a service is under load, indexing might fail for some documents in the batch.   
                // Depending on your application, you can compensate by delaying and retrying.   
                // For this simple demo, we just log the failed document keys and continue.  
                Console.WriteLine("Failed to index some of the documents: {0}",
                    String.Join(", ", e.Message));
            }
            // Wait 2 seconds before starting queries  
            Console.WriteLine("Waiting for indexing...\n");
            Thread.Sleep(2000);

            return result;
        }

        private  SearchServiceClient CreateSearchServiceClient(string searchServiceName, string adminApiKey)
        {
            SearchCredentials creds = new SearchCredentials(adminApiKey);
            SearchServiceClient serviceClient = new SearchServiceClient(searchServiceName, creds);
            return serviceClient;
        }



        /* public async Task<int> CreateIndex(string indexName, SearchIndexClient adminClient)
         {
             int result = 0;

             try
             {
                 FieldBuilder fieldBuilder = new FieldBuilder();
                 var searchFields = fieldBuilder.Build(typeof(Customer));

                 var definition = new SearchIndex(indexName, searchFields);

                 var suggester = new SearchSuggester("Name", new[] { "Name", "Address", "Email", "Phone" });

                 definition.Suggesters.Add(suggester);

                await adminClient.CreateOrUpdateIndexAsync(definition);
             }
             catch(Exception ex)
             {

             }

             return result;
         }

         public async Task<int> DeleteIndexIfExists(string indexName, SearchIndexClient adminClient)
         {
             int result = 0;

             try
             {
                 adminClient.GetIndexNames();
                 {
                   await  adminClient.DeleteIndexAsync(indexName);
                 }
             }
             catch(Exception ex)
             {
             }

             return result;
         }

         public async Task<int> UploadDocuments(SearchClient ingesterClient , List<Customer> customers)
         {
             int result = 0;

            // IndexDocumentsBatch<Customer> batch = null;

             var actions = new IndexAction<Customer>[customers.Count()];

             int i = 0;
             Customer customes;
             try
             {
                 foreach (var cust in customers)
                 {
                     var custResult = IndexDocumentsAction.Upload(

                     customes= new Customer()
                      {
                          Id = cust.Id,
                          Name = cust.Name,
                          Email = cust.Email,
                          Address = cust.Address,
                          Phone = cust.Phone,
                          CreateTs = cust.CreateTs,
                          CreateUser = cust.CreateUser,
                          UpdateTs = cust.UpdateTs,
                          UpdateUser = cust.UpdateUser
                      }
                      );
                     actions[i] = IndexAction.Upload(customes);
                     i++;

                    // batch = IndexDocumentsBatch.Create<Customer>(custResult);
                 }
                 var batch = IndexBatch.New(actions);

                 indexClient.Documents.Index(batch);

             }
             catch (Exception)
             {
                 // If for some reason any documents are dropped during indexing, you can compensate by delaying and
                 // retrying. This simple demo just logs the failed document keys and continues.
                 Console.WriteLine("Failed to index some of the documents: {0}");
             }
             return result;
         }*/
    }
}
