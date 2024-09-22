namespace PosSystem.Application.Contracts.InvoiceItem
{
    public class InvoiceItemShortOutContract
    {
        public string InvoiceItemId { get; set; }
        public string ItemName { get; set; }
        public decimal Quantity { get; set; }
    }
}
