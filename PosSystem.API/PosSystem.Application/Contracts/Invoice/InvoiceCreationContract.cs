using PosSystem.Application.Contracts.InvoiceItem;
using PosSystem.Application.Contracts.Validations.Invoice;
using System.ComponentModel.DataAnnotations;

namespace PosSystem.Application.Contracts.Invoice
{
    public class InvoiceCreationContract
    {
        [Required]
        public DateTime Date { get; set; } = DateTime.Now;
        public DateTime BillDate { get; set; }
       
        [Range(0, int.MaxValue)]
        public decimal PaidUp { get; set; }
        
        
        [Range(0, int.MaxValue)]
        public decimal TotalDiscount { get; set; }
        
        public decimal TotalAmount { get; set; }
        
        [Required]
        public List<InvoiceItemCreationContract> InvoiceItems { get; set; } = [];

        [ExistingClientId]
        public string ClientId { get; set; }
        
        // [Required]
        public string? EmployeeId { get; set; }
    }
}
