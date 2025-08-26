namespace ProductsAPI.ProductManagement.DbObjects
{
    public class Product
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Price { get; set; }
    }
}
