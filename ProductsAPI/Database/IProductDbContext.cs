using Microsoft.EntityFrameworkCore;
using ProductsAPI.Authentication.DbObjects;

namespace ProductsAPI.Database
{
    public interface IProductDBContext
    {
        DbSet<User> Users { get; set; }

        public int SaveChanges();
    }
}