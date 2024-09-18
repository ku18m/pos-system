using PosSystem.Application.Contracts.Unit;
using PosSystem.Application.Interfaces.IRepositories;
using System.ComponentModel.DataAnnotations;

namespace PosSystem.Application.Contracts.Validations.Unit
{
    public class UniqueUnitNameOnUpdateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string name)
            {
                var unitRepo = validationContext.GetService(typeof(IUnitRepository)) as IUnitRepository;

                if (unitRepo == null)
                {
                    throw new InvalidOperationException("IClientRepository service is not available.");
                }

                var contract = (UnitOperationsContract)validationContext.ObjectInstance;

                var unit = unitRepo.GetUnitByName(name).Result;

                if (unit != null && unit.UnitId != contract.Id)
                {
                    return new ValidationResult("This unit name is already taken.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
