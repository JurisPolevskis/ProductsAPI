using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsAPI.ProductManagement.DbObjects
{
    [Table("product_audit")]
    public class ProductAudit
    {
        public int Id { get; set; }
        public string? ChangeType { get; set; }
        public int? ProductId { get; set; }
        public string? OldTitle { get; set; }
        public decimal? OldQuantity { get; set; }
        public decimal? OldPrice { get; set; }
        public string? NewTitle { get; set; }
        public decimal? NewQuantity { get; set; }
        public decimal? NewPrice { get; set; }
        public string? Source { get; set; }
        public string? User { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
