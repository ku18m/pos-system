using PosSystem.Application.Interfaces.IRepositories;
using System.ComponentModel.DataAnnotations;

namespace PosSystem.Application.Contracts.Validations.Unit
{
    public class UniqueUnitNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string name)
            {
                var unitRepo = validationContext.GetService(typeof(IUnitRepository)) as IUnitRepository;

                if (unitRepo == null)
                {
                    throw new InvalidOperationException("IUnitRepository service is not available.");
                }

                var unit = unitRepo.GetUnitByName(name).Result;

                if (unit != null)
                {
                    return new ValidationResult("This unit name is already taken.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
