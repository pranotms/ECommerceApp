using Microsoft.EntityFrameworkCore;
using ECommerceApp.Model;

namespace ECommerceApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
