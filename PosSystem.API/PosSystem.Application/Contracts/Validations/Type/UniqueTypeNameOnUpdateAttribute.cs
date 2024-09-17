using PosSystem.Application.Contracts.Type;
using PosSystem.Application.Interfaces.IRepositories;
using System.ComponentModel.DataAnnotations;

namespace PosSystem.Application.Contracts.Validations.Type
{
    public class UniqueTypeNameOnUpdateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string name)
            {
                var companyRepo = validationContext.GetService(typeof(ICategoryRepository)) as ICategoryRepository;

                if (companyRepo == null)
                {
                    throw new InvalidOperationException("ICategoryRepository service is not available.");
                }

                var contract = (TypeOperationsContract)validationContext.ObjectInstance;

                var company = companyRepo.GetCategoryByName(name).Result;

                if (company != null && company.CategoryId != contract.Id)
                {
                    return new ValidationResult("This type name is already taken.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
