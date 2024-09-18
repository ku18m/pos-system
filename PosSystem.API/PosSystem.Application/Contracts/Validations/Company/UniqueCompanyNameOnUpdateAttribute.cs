using PosSystem.Application.Contracts.Company;
using PosSystem.Application.Interfaces.IRepositories;
using System.ComponentModel.DataAnnotations;

namespace PosSystem.Application.Contracts.Validations.Company
{
    public class UniqueCompanyNameOnUpdateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string name)
            {
                var companyRepo = validationContext.GetService(typeof(ICompanyRepository)) as ICompanyRepository;

                if (companyRepo == null)
                {
                    throw new InvalidOperationException("IClientRepository service is not available.");
                }

                var contract = (CompanyOperationsContract)validationContext.ObjectInstance;

                var company = companyRepo.GetCompanyByName(name).Result;

                if (company != null && company.CompanyId != contract.Id)
                {
                    return new ValidationResult("This company name is already taken.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
