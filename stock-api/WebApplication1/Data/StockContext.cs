using Microsoft.EntityFrameworkCore;
using StockAPI.Models;

namespace StockAPI.Data
{
    public class StockContext : DbContext
    {
        public StockContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

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
