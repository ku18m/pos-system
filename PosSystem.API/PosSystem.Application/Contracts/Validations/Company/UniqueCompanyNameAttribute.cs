using PosSystem.Application.Interfaces.IRepositories;
using System.ComponentModel.DataAnnotations;

namespace PosSystem.Application.Contracts.Validations.Company
{
    public class UniqueCompanyNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string name)
            {
                var companyRepo = validationContext.GetService(typeof(ICompanyRepository)) as ICompanyRepository;

                if (companyRepo == null)
                {
                    throw new InvalidOperationException("ICompanyRepository service is not available.");
                }

                var company = companyRepo.GetCompanyByName(name).Result;

                if (company != null)
                {
                    return new ValidationResult("This company name is already taken.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
