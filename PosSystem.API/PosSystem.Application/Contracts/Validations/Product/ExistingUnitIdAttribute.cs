using PosSystem.Application.Interfaces.IRepositories;
using System.ComponentModel.DataAnnotations;

namespace PosSystem.Application.Contracts.Validations.Product
{
    public class ExistingUnitIdAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var unitId = value as string;

            if (unitId == null)
            {
                return new ValidationResult("Unit Id is required");
            }

            var unitRepo = validationContext.GetService(typeof(IUnitRepository)) as IUnitRepository;

            if (unitRepo == null)
            {
                return new ValidationResult("Unit Repository not found");
            }

            var unit = unitRepo.GetById(unitId).Result;

            if (unit == null)
            {
                return new ValidationResult("Unit not found");
            }

            return ValidationResult.Success;
        }
    }
}
