using PosSystem.Application.Interfaces.IRepositories;
using System.ComponentModel.DataAnnotations;

namespace PosSystem.Application.Contracts.Validations.Product
{
    public class ExistingTypeIdAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var categoryId = value as string;

            if (categoryId == null)
            {
                return new ValidationResult("Category Id is required");
            }

            var categoryRepo = validationContext.GetService(typeof(ICategoryRepository)) as ICategoryRepository;

            if (categoryRepo == null)
            {
                return new ValidationResult("Category Repository not found");
            }

            var category = categoryRepo.GetById(categoryId).Result;

            if (category == null)
            {
                return new ValidationResult("Category not found");
            }

            return ValidationResult.Success;
        }
    }
}
