using PosSystem.Application.Contracts.InvoiceItem;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Application.Contracts.Invoice
{
    public class InvoiceOperationsContract
    {
        public string Id { get; set; }

        [Required]
        public DateTime BillDate { get; set; } = DateTime.Now;

        [Range(0, int.MaxValue)]
        public int PaidUp { get; set; }

        [Range(0, int.MaxValue)]
        public int Net { get; set; }

        [Range(0, int.MaxValue)]
        public int DiscountValue { get; set; }

        [Range(0, 100)]
        public int DiscountPercentage { get; set; }
        public int BillsTotal { get; set; }
        
        [Required]
        public List<InvoiceItemCreationContract> InvoiceItems { get; set; } = new List<InvoiceItemCreationContract>();

        [Required]
        public string ClientId { get; set; }

        [Required]
        public string EmployeeId { get; set; }
    }
}
