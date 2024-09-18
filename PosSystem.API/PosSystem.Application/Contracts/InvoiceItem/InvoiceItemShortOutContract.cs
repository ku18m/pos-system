using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Application.Contracts.InvoiceItem
{
    public class InvoiceItemShortOutContract
    {
        public string ItemName { get; set; }
        public int Quantity { get; set; }
    }
}
