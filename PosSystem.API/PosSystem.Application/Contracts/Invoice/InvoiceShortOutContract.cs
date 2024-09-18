using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Application.Contracts.Invoice
{
    public class InvoiceShortOutContract
    {
        public string Id { get; set; }
        public DateTime BillDate { get; set; } 
    }
}
