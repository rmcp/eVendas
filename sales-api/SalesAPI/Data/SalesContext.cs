using Microsoft.EntityFrameworkCore;
using SalesAPI.Models;

namespace SalesAPI.Data
{
    public class SalesContext : DbContext
    {
        public SalesContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Sale> Sales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.Entity<Product>()
           .HasIndex(c => c.Code)
           .IsUnique();

            modelBuilder.Entity<Product>()
           .HasIndex(c => c.Name)
           .IsUnique();            

        }

    }
}
