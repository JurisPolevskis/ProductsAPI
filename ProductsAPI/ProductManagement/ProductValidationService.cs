using Microsoft.IdentityModel.Tokens;
using ProductsAPI.ProductManagement.Dtos;
using System.Text;

namespace ProductsAPI.ProductManagement
{
    public class ProductValidationService : IProductValidationService
    {
        public bool Validate(Product product, out string errorMessage)
        {
            var errorBuilder = new StringBuilder();
            if (string.IsNullOrWhiteSpace(product.ItemName))
            {
                errorBuilder.AppendLine("Item name should not be null or whitespace");
            }
            else if (product.ItemName.Length > Database.Constants.Limits.MaxStringSize)
            {
                errorBuilder.AppendLine("Item name should not be longer than {Database.Constants.Limits.MaxStringSize} symbols");
            }

            if (product.Quantity < 0)
            {
                errorBuilder.AppendLine($"Quantity {product.Quantity} should not be less than than 0");
            }
            if (product.Quantity > Database.Constants.Limits.MaxDecimal)
            {
                errorBuilder.AppendLine($"Quantity {product.Quantity} should not be more than than {Database.Constants.Limits.MaxDecimal}");
            }

            if (product.Price < 0.01M)
            {
                errorBuilder.AppendLine($"Price {product.Price} should not be less than than 0.01");
            }
            if (product.Quantity > Database.Constants.Limits.MaxDecimal)
            {
                errorBuilder.AppendLine($"Price {product.Price} should not be more than than {Database.Constants.Limits.MaxDecimal}");
            }

            errorMessage = errorBuilder.ToString();
            return errorMessage.IsNullOrEmpty();
        }

    }
}
