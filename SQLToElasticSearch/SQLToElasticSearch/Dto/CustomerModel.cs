using Microsoft.Azure.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLToElasticSearch.Dto
{
    public class CustomerModel
    {
        [System.ComponentModel.DataAnnotations.Key]
        [IsFilterable]
        public string ID { get; set; }
        [IsSearchable, IsFilterable, IsSortable, IsFacetable]
        public string NAME { get; set; }
        [IsSearchable, IsFilterable, IsSortable, IsFacetable]
        public string PhoneNumber { get; set; }
        [IsSearchable, IsFilterable, IsSortable, IsFacetable]
        public string Email { get; set; }
        [IsSearchable, IsFilterable, IsSortable, IsFacetable]
        public string Address { get; set; }
    }
}
