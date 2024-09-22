using PosSystem.Application.Contracts.InvoiceItem;
using PosSystem.Application.Interfaces.IRepositories;
using System.ComponentModel.DataAnnotations;

namespace PosSystem.Application.Contracts.Validations.InvoiceItem
{
    public class QuantityAvailableInStockAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is decimal quantity)
            {
                var productRepo = validationContext.GetService(typeof(IProductRepository)) as IProductRepository;

                if (productRepo == null)
                {
                    throw new InvalidOperationException("IProductRepository service is not available.");
                }

                var contract = (InvoiceItemCreationContract)validationContext.ObjectInstance;

                var product = productRepo.GetById(contract.ItemId).Result;

                if (product == null)
                {
                    return new ValidationResult($"The product with id {contract.ItemId} isn't available");
                }

                if (product.Quantity < quantity)
                {
                    return new ValidationResult($"The quantity of the product ( {product.Name} ) isn't available in stock");
                }
            }

            return ValidationResult.Success;
        }
    }
}
