using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace PosSystem.Core.Entities
{
    public class Product
    {
        [Key]
        public string ProductId { get; set; }

        public string Name { get; set; }

        public decimal SellingPrice { get; set; }

        public decimal BuyingPrice { get; set; }

        public decimal Quantity { get; set; }

        public string? Notes { get; set; }

        [ForeignKey("Category")]
        public string CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [ForeignKey("Company")]
        public string CompanyId { get; set; }
        public virtual Company Company { get; set; }

        [ForeignKey("Unit")]
        public string UnitId { get; set; }
        public virtual Unit Unit { get; set; }

        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }

        public Product()
        {
            ProductId = Guid.NewGuid().ToString("N").Substring(0, 6);
        }
    }
}
