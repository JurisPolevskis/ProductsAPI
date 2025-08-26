using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProductsAPI.Authentication.DbObjects;
using ProductsAPI.ProductManagement.DbObjects;

namespace ProductsAPI.Database
{
    public class ProductDBContext(DbContextOptions<ProductDBContext> options) : DbContext(options), IProductDBContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductAudit> ProductAudit { get; set; }

        public int SaveChanges(HttpContext context)
        {
            var changedProducts = ChangeTracker.Entries<Product>();

            var auditEntries = changedProducts.Select(product => CreateAudit(product, context)).ToList();

            foreach (var entry in auditEntries)
            {
                ProductAudit.Add(entry);
            }

            return base.SaveChanges();
        }

        private ProductAudit CreateAudit(EntityEntry<Product> changedProduct, HttpContext context)
        {
            if (changedProduct is null)
                throw new ArgumentNullException(nameof(changedProduct));

            var source = $"{context.Request.Method} {context.Request.Path}";


            var productAudit = new ProductAudit
            {
                ChangeType = Enum.GetName(changedProduct.State),
                Source = source,
                User = context.User.Identity.Name,
                ModificationDate = DateTime.UtcNow
            };

            if (changedProduct.State != EntityState.Added)
            {
                productAudit.ProductId = (int)changedProduct.CurrentValues["Id"];
            }

            if (changedProduct.State == EntityState.Added || changedProduct.State == EntityState.Modified)
            {
                productAudit.NewTitle = (string)changedProduct.CurrentValues["Title"];
                productAudit.NewQuantity = (decimal)changedProduct.CurrentValues["Quantity"];
                productAudit.NewPrice = (decimal)changedProduct.CurrentValues["Price"];
            }

            if (changedProduct.State == EntityState.Deleted || changedProduct.State == EntityState.Modified)
            {
                productAudit.OldTitle = (string)changedProduct.OriginalValues["Title"];
                productAudit.OldQuantity = (decimal)changedProduct.OriginalValues["Quantity"];
                productAudit.OldPrice = (decimal)changedProduct.OriginalValues["Price"];
            }

            return productAudit;
        }
    }
}
