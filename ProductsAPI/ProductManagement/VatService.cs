using System.Globalization;

namespace ProductsAPI.ProductManagement
{
    public class VatService : IVatService
    {
        private readonly double vatRate;
        public VatService(IConfiguration config)
        {
            var vatRateStr = config["VatRate"];
            if (!double.TryParse(vatRateStr, NumberStyles.Any, CultureInfo.InvariantCulture, out vatRate))
            {
                throw new InvalidDataException("Invalid VatRate in config");
            }
        }

        public decimal? GetTotalPriceWithVat(DbObjects.Product product)
        {
            if (product.Price == null || product.Quantity == null)
            {
                return null;
            }
            double priceBeforeVat = (double)(product.Quantity * product.Price);
            //TODO: Contact business side and clarify what kind of rounding should we apply
            return (decimal)(priceBeforeVat * (1 + vatRate));
        }
    }
}
