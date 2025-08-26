using Microsoft.EntityFrameworkCore;
using ProductsAPI.Authentication.DbObjects;
using ProductsAPI.ProductManagement.DbObjects;

namespace ProductsAPI.Database
{
    public class ProductDBContext(DbContextOptions<ProductDBContext> options) : DbContext(options), IProductDBContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Product> Product { get; set; }
    }
}
