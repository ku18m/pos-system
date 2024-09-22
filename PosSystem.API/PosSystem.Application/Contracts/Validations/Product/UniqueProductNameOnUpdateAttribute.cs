using PosSystem.Application.Contracts.Product;
using PosSystem.Application.Interfaces.IRepositories;
using System.ComponentModel.DataAnnotations;

namespace PosSystem.Application.Contracts.Validations.Product
{
    public class UniqueProductNameOnUpdateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string name)
            {
                var productRepo = validationContext.GetService(typeof(IProductRepository)) as IProductRepository;

                if (productRepo == null)
                {
                    throw new InvalidOperationException("IClientRepository service is not available.");
                }

                var contract = (ProductOperationsContract)validationContext.ObjectInstance;

                var product = productRepo.GetProductByName(name).Result;

                if (product != null && product.ProductId != contract.Id)
                {
                    return new ValidationResult("This product name is already taken.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
