using PosSystem.Core.Entities;

namespace PosSystem.API.DTO
{
    public class InvoiceDTO
    {
        public DateTime BillDate { get; set; }

        public int BillNumber { get; set; }

        public decimal Price { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal TotalDiscount { get; set; }

        public decimal FinalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal DueAmount { get; set; }

        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public virtual ICollection<InvoiceItemDTO> Items { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<ClientDTO> Clients { get; set; }




    }
}
