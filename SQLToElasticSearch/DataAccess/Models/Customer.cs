using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateTs { get; set; }
        public string UpdateUser { get; set; }
        public DateTime UpdateTs { get; set; }
    }
}
