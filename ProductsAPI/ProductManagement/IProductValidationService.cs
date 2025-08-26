using ProductsAPI.ProductManagement.Dtos;

namespace ProductsAPI.ProductManagement
{
    public interface IProductValidationService
    {
        bool Validate(Product product, out string errorMessage);
    }
}