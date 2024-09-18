using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Application.Contracts.InvoiceItem
{
    public class InvoiceItemOutContract
    {
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }
        public decimal SellingPrice { get; set; }

    }
}
