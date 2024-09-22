using PosSystem.Application.Interfaces.IRepositories;
using System.ComponentModel.DataAnnotations;

namespace PosSystem.Application.Contracts.Validations.Product
{
    public class UniqueProductNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string name)
            {
                var productRepo = validationContext.GetService(typeof(IProductRepository)) as IProductRepository;

                if (productRepo == null)
                {
                    throw new InvalidOperationException("IProductRepository service is not available.");
                }

                var product = productRepo.GetProductByName(name).Result;

                if (product != null)
                {
                    return new ValidationResult("This product name is already taken.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
