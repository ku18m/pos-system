using PosSystem.Application.Contracts.InvoiceItem;

namespace PosSystem.Application.Contracts.Invoice
{
    public class InvoiceOutContract
    {
        public string Id { get; set; }
        public int Number { get; set; }

        public DateTime BillDate { get; set; } 
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal Net { get; set; }
        public decimal PaidUp { get; set; }
        public decimal Remaining { get; set; }
        public List<InvoiceItemOutContract> InvoiceItems { get; set; } = new List<InvoiceItemOutContract>();
        public string ClientName { get; set; }
        public string EmployeeName { get; set; }
    }
}
