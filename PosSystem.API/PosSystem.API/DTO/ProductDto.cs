
namespace PosSystem.API.DTO
{
    public class ProductDTO
    {
        public string Id { get; set; }
        public string ProductName { get; set; }

        public decimal SellingPrice { get; set; }

        public decimal BuyingPrice { get; set; }

        public string? Notes { get; set; }
        public virtual List<CompanyDTO> Companies { get; set; }
        public virtual List<CategoryDTO> Categories { get; set; }


    }
}
