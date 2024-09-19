using PosSystem.Application.Contracts.Validations.InvoiceItem;
using PosSystem.Application.Contracts.Validations.Product;
using System.ComponentModel.DataAnnotations;

namespace PosSystem.Application.Contracts.InvoiceItem
{
    public class InvoiceItemCreationContract
    {
        [Required]
        public string ItemId { get; set; }
        
        [Range(0, int.MaxValue)]
        [QuantityAvailableInStock]
        public decimal Quantity { get; set; }

        [ExistingUnitId]
        public string UnitId { get; set; }

        [Range(0, int.MaxValue)]
        public decimal SellingPrice { get; set; }
    }
}
