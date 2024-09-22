using PosSystem.Application.Contracts.InvoiceItem;
using PosSystem.Application.Interfaces.IRepositories;
using System.ComponentModel.DataAnnotations;

namespace PosSystem.Application.Contracts.Validations.InvoiceItem
{
    public class QuantityAvailableInStockOnUpdateAttribute : ValidationAttribute
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

                var invoiceItemRepo = validationContext.GetService(typeof(IInvoiceItemRepository)) as IInvoiceItemRepository;

                if (invoiceItemRepo == null)
                {
                    throw new InvalidOperationException("IInvoiceItemRepository service is not available.");
                }

                var contract = (InvoiceItemOperationsContract)validationContext.ObjectInstance;

                var product = productRepo.GetById(contract.ItemId).Result;

                if (product == null)
                {
                    return new ValidationResult($"The product with id {contract.ItemId} isn't available");
                }

                var invoiceItem = invoiceItemRepo.GetById(contract.InvoiceItemId).Result;

                if (invoiceItem == null)
                {
                    return new ValidationResult($"The invoice item with id {contract.InvoiceItemId} isn't available");
                }

                if (product.Quantity < (quantity - invoiceItem.Quantity))
                {
                    return new ValidationResult($"The quantity of the product ( {product.Name} ) isn't available in stock");
                }
            }

            return ValidationResult.Success;
        }
    }
}
