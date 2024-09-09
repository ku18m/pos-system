using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Core.Entities
{
    public class Product
    {
        [Key]
        public string ProductId { get; set; }


        [Required(ErrorMessage = "Product Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Selling Price must be greater than or equal to 0")]
        [Range(0, double.MaxValue)]
        public decimal SellingPrice { get; set; }

        [Required(ErrorMessage = "Buying Price must be greater than or equal to 0 and less than or equal to Selling Price")]
        [Range(0, double.MaxValue)]
        public decimal BuyingPrice { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
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
