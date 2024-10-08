﻿namespace PosSystem.Core.Entities
{
    public class Invoice
    {
        public string Id { get; private set; }

        public DateTime BillDate { get; set; }

        public int BillNumber { get; set; }

        public string ClientId { get; set; }
        public virtual Client Client { get; set; }

        public virtual ICollection<InvoiceItem> Items { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal TotalDiscount { get; set; }

        public decimal FinalAmount { get; set; }

        public decimal PaidAmount { get; set; }

        public decimal DueAmount { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public DateTime Date { get; set; }

        public Invoice()
        {
            Id = Guid.NewGuid().ToString();
            Items = new List<InvoiceItem>();
        }
    }
}
