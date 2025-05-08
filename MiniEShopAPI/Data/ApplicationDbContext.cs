/**
 * ApplicationDbContext manages database access and entity configurations.
 */
using Microsoft.EntityFrameworkCore;
using MiniEShopAPI.Models;

namespace MiniEShopAPI.Data
{
    /**
     * ApplicationDbContext manages database access and entity configurations.
     */
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            // Configures the database context with the provided options
        }

        // DbSet for Product entity
        public DbSet<Product> Products { get; set; } // Represents the Products table in the database

        // DbSet for Order entity
        public DbSet<Order> Orders { get; set; } // Represents the Orders table in the database

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Calls the base implementation
            // Additional configurations can be added here
        }
    }
}