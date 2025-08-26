using System.Text.Json.Serialization;

namespace ProductsAPI.ProductManagement.Dtos
{
    public record Product
    {
        [JsonPropertyName("Item name")]
        public string? ItemName { get; set; }
        [JsonPropertyName("Quantity")]
        public decimal? Quantity { get; set; }
        [JsonPropertyName("Price")]
        public decimal? Price { get; set; }
        [JsonPropertyName("Total price with VAT")]
        public decimal? TotalPriceWithVat { get; set; }
    }
}
