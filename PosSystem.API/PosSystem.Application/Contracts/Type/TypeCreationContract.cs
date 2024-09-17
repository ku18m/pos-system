
using PosSystem.Application.Contracts.Validations.Type;

namespace PosSystem.Application.Contracts.Type
{
    public class TypeCreationContract
    {
        [UniqueTypeName]
        public string Name { get; set; }

        public string? Note { get; set; }

        [ExistingCompanyId]
        public string CompanyId { get; set; }
    }
}
