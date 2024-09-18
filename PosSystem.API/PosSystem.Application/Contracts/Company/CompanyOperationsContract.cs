using PosSystem.Application.Contracts.Validations.Company;

namespace PosSystem.Application.Contracts.Company
{
    public class CompanyOperationsContract
    {
        public string Id { get; set; }

        [UniqueCompanyNameOnUpdate]
        public string Name { get; set; }

        public string? Note { get; set; }
    }
}
