using Microsoft.EntityFrameworkCore;
using SportStore.Core.Entities;

namespace SportStore.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        // if you replace DbContextOptions<AppDbContext> -> DbContextOptions app will crash
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}