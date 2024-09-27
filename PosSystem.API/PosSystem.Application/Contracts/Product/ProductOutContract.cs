namespace PosSystem.Application.Contracts.Product
{
    public class ProductOutContract
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal SellingPrice { get; set; }

        public decimal BuyingPrice { get; set; }

        public decimal Quantity { get; set; }

        public string? Notes { get; set; }

        public string CategoryName { get; set; }

        public string CompanyName { get; set; }

        public string UnitName { get; set; }
    }
}
