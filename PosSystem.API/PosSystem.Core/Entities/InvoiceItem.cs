using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Core.Entities
{
    public class InvoiceItem
    {
        public string Id { get; private set; }

        public string InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }

        public string ProductId { get; set; }
        public virtual Product Product { get; set; }

        public string UnitId { get; set; }
        public virtual Unit Unit { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal Discount { get; set; }

        public decimal FinalAmount { get; set; }


        public InvoiceItem()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
