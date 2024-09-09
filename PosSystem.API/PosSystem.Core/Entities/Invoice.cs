using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Core.Entities
{
    public class Invoice
    {
        public string Id { get; private set; }

        public DateTime BillDate { get; set; }

        public long BillNumber { get; set; }

        public string ClientId { get; set; }
        public virtual Client Client { get; set; }

        public virtual ICollection<InvoiceItem> Items { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal TotalDiscount { get; set; }

        public decimal FinalAmount { get; set; }

        public decimal PaidAmount { get; set; }

        public decimal DueAmount { get; set; }

        public string EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public Invoice()
        {
            Id = Guid.NewGuid().ToString();
            Items = new List<InvoiceItem>();
        }
    }
}
