using App.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace App.DataAccess.Context
{
    public class AppContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        
        public DbSet<Contract> Contracts { get; set; }
        
        public DbSet<Delivery> Deliveries { get; set; }
        
        public DbSet<Item> Items { get; set; }

        public AppContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}