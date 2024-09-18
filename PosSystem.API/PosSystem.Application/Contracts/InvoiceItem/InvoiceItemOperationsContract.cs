using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Application.Contracts.InvoiceItem
{
    public class InvoiceItemOperationsContract
    {
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(0, int.MaxValue)]
        public int Total { get; set; }

        [Range(0, int.MaxValue)]
        public int SellingPrice { get; set; }

        [Required]
        public string ItemId { get; set; }

        [Required]

        public string InvoiceId { get; set; }
    }
}
