using PosSystem.Application.Interfaces.IRepositories;
using System.ComponentModel.DataAnnotations;

namespace PosSystem.Application.Contracts.Validations.Type
{
    public class ExistingCompanyIdAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var companyId = value as string;

            if (companyId == null)
            {
                return new ValidationResult("Company Id is required");
            }

            var companyRepo = validationContext.GetService(typeof(ICompanyRepository)) as ICompanyRepository;

            if (companyRepo == null)
            {
                return new ValidationResult("Company Repository not found");
            }

            var company = companyRepo.GetById(companyId).Result;

            if (company == null)
            {
                return new ValidationResult("Company not found");
            }

            return ValidationResult.Success;
        }
    }
}
