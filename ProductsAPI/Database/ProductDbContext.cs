using Microsoft.EntityFrameworkCore;
using ProductsAPI.Authentication.DbObjects;

namespace ProductsAPI.Database
{
    public class ProductDBContext(DbContextOptions<ProductDBContext> options) : DbContext(options), IProductDBContext
    {
        public virtual DbSet<User> Users { get; set; }
    }
}
