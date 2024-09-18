using PosSystem.Application.Contracts.Validations.Company;

namespace PosSystem.Application.Contracts.Company
{
    public class CompanyCreationContract
    {
        [UniqueCompanyName]
        public string Name { get; set; }

        public string? Note { get; set; }
    }
}
