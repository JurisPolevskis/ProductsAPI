using ProductsAPI.ProductManagement.DbObjects;

namespace ProductsAPI.ProductManagement
{
    public interface IVatService
    {
        decimal? GetTotalPriceWithVat(Product product);
    }
}