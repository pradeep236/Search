using Core_MVC_AutoComplete.Models;
using Microsoft.EntityFrameworkCore;
namespace Core_MVC_AutoComplete
{
    public class DBCtx : DbContext
    {
        public DBCtx(DbContextOptions<DBCtx> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
    }
}