namespace PosSystem.Application.Contracts.InvoiceItem
{
    public class InvoiceItemOutContract
    {
        public string InvoiceItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemId { get; set; }
        public string UnitName { get; set; }
        public string UnitId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Total { get; set; }
        public decimal SellingPrice { get; set; }
    }
}
