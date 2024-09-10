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

        public int Quantity { get; set; }

        public string? Notes { get; set; }

        [ForeignKey("Type")]
        public int TypeId { get; set; }
        public virtual Type Type { get; set; }

        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }

        [ForeignKey("Unit")]
        public int UnitId { get; set; }
        public virtual Unit Unit { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }

        [ForeignKey("Employee")]
        public int? EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        [ForeignKey("Client")]
        public int? ClientId { get; set; }
        public virtual Client Client { get; set; }
        public Product()
        {
            ProductId = Guid.NewGuid().ToString();
        }
    }
}
