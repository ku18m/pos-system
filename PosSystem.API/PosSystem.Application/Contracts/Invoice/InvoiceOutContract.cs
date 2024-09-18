using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Application.Contracts.Invoice
{
    public class InvoiceOutContract
    {
        public string Id { get; set; }
        public int Number { get; set; }

        public DateTime BillDate { get; set; } 
        public int PaidUp { get; set; }
        public int Net { get; set; }
        public int DiscountValue { get; set; }
        public int DiscountPercentage { get; set; }
        public int BillsTotal { get; set; }
        public string ClientName { get; set; }
        public string EmployeeName { get; set; }
    }
}
