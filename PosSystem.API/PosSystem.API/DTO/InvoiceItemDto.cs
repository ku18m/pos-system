using System.ComponentModel.DataAnnotations;

namespace PosSystem.API.DTO
{
    public class InvoiceItemDTO
    {
        public string Id { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public decimal FinalAmount { get; set; }



    }
}
