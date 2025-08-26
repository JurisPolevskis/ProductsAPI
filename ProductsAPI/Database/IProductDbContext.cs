using Microsoft.EntityFrameworkCore;
using ProductsAPI.Authentication.DbObjects;
using ProductsAPI.ProductManagement.DbObjects;

namespace ProductsAPI.Database
{
    public interface IProductDBContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Product> Product { get; set; }

        public int SaveChanges(HttpContext context);
    }
}