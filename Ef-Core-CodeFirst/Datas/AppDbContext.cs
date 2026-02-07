using Ef_Core_CodeFirst.Models.Entities.Concretes;
using Microsoft.EntityFrameworkCore;

namespace Ef_Core_CodeFirst.Datas
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
