using API_Assignment_I.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Assignment_I.Data
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext()
        {

        }

        public CustomerDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Customer> customers { get; set; }
    }
}
