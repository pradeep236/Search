using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class Address
    {
        public int AddressId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateTs { get; set; }
        public string UpdateUser { get; set; }
        public DateTime UpdateTs { get; set; }
    }
}
