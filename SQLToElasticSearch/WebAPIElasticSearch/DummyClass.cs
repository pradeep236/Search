using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIElasticSearch
{

    //public interface IElasticServiceClient
    //{
    //    Task<int> RunQueries(SearchClient searchClient);
    //}
    //public class ElasticServiceClient : IElasticServiceClient
    //{
    //    AzureKeyCredential credential = null;
    //    SearchIndexClient adminClient = null;
    //    Uri serviceEndpoint = null;
    //    SearchClient searchClient = null;

    //    public ElasticServiceClient()
    //    {
    //        string serviceName = "<Put your search service NAME here>";
    //        string apiKey = "<Put your search service ADMIN API KEY here";
    //        string indexName = "hotels-quickstart";

    //        // Create a SearchIndexClient to send create/delete index commands
    //        serviceEndpoint = new Uri($"https://{serviceName}.search.windows.net/");
    //        credential = new AzureKeyCredential(apiKey);
    //        adminClient = new SearchIndexClient(serviceEndpoint, credential);

    //        // Create a SearchClient to load and query documents
    //        searchClient = new SearchClient(serviceEndpoint, indexName, credential);
    //    }

    //    public async Task<int> RunQueries(SearchClient searchClient)
    //    {
    //        int result = 0;

    //        SearchOptions options;
    //        SearchResults<CustomerDto> response;

    //        // Query 1
    //        Console.WriteLine("Query #1: Search on empty term '*' to return all documents, showing a subset of fields...\n");

    //        options = new SearchOptions()
    //        {
    //            IncludeTotalCount = true,
    //            Filter = "",
    //            OrderBy = { "" }
    //        };

    //        options.Select.Add("Phone");
    //        options.Select.Add("Email");
    //        options.Select.Add("Address");
    //        options.Select.Add("Name");

    //        response = searchClient.Search<CustomerDto>("*", options);
    //        // WriteDocuments(response);

    //        // Query 2
    //        Console.WriteLine("Query #2: Search on 'hotels', filter on 'Rating gt 4', sort by Rating in descending order...\n");

    //        options = new SearchOptions()
    //        {
    //            Filter = "Rating gt 4",
    //            OrderBy = { "Rating desc" }
    //        };

    //        options.Select.Add("Phone");
    //        options.Select.Add("Email");
    //        options.Select.Add("Address");
    //        options.Select.Add("Name");

    //        response = searchClient.Search<CustomerDto>("customer", options);
    //        // WriteDocuments(response);

    //        // Query 3
    //        Console.WriteLine("Query #3: Limit search to specific fields (pool in Tags field)...\n");

    //        options = new SearchOptions()
    //        {
    //            SearchFields = { "Name" }
    //        };

    //        options.Select.Add("Phone");
    //        options.Select.Add("Email");
    //        options.Select.Add("Address");
    //        options.Select.Add("Name");

    //        response = searchClient.Search<CustomerDto>("customer", options);
    //        // WriteDocuments(response);

    //        // Query 4 - Use Facets to return a faceted navigation structure for a given query
    //        // Filters are typically used with facets to narrow results on OnClick events
    //        Console.WriteLine("Query #4: Facet on 'Category'...\n");

    //        options = new SearchOptions()
    //        {
    //            Filter = ""
    //        };

    //        options.Select.Add("Phone");
    //        options.Select.Add("Email");
    //        options.Select.Add("Address");
    //        options.Select.Add("Name");

    //        response = searchClient.Search<CustomerDto>("*", options);
    //        // WriteDocuments(response);

    //        // Query 5
    //        Console.WriteLine("Query #5: Look up a specific document...\n");

    //        Response<CustomerDto> lookupResponse;
    //        lookupResponse = searchClient.GetDocument<CustomerDto>("3");

    //        Console.WriteLine(lookupResponse.Value.Name);

    //        // Query 6
    //        Console.WriteLine("Query #6: Call Autocomplete on HotelName...\n");

    //        var autoresponse = searchClient.Autocomplete("sa", "sg");
    //        //WriteDocuments(autoresponse);
    //        result = 1;
    //        return result;
    //    }

    //    // Write search results to console
    //    //private static void WriteDocuments(SearchResults<Hotel> searchResults)
    //    //{
    //    //    foreach (SearchResult<Hotel> result in searchResults.GetResults())
    //    //    {
    //    //        Console.WriteLine(result.Document);
    //    //    }

    //    //    Console.WriteLine();
    //    //}

    //    //private static void WriteDocuments(AutocompleteResults autoResults)
    //    //{
    //    //    foreach (AutocompleteItem result in autoResults.Results)
    //    //    {
    //    //        Console.WriteLine(result.Text);
    //    //    }

    //    //    Console.WriteLine();
    //    //}
    //}
}
