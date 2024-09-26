using PosSystem.Application.Contracts.Validations.Product;
using PosSystem.Application.Contracts.Validations.Type;
using System.ComponentModel.DataAnnotations;

namespace PosSystem.Application.Contracts.Product
{
    public class ProductCreationContract
    {
        [Required]
        [UniqueProductName]
        public string Name { get; set; }

        [Required]
        public decimal BuyingPrice { get; set; }

        [Required]
        [SellingNotLessThanBuying]
        public decimal SellingPrice { get; set; }


        [Required]
        [Range(0, double.MaxValue)]
        public decimal? Quantity { get; set; }

        public string? Notes { get; set; }

        [Required]
        [ExistingTypeId]
        public string CategoryId { get; set; }

        [Required]
        [ExistingCompanyId]
        public string CompanyId { get; set; }

        [Required]
        [ExistingUnitId]
        public string UnitId { get; set; }
    }
}
