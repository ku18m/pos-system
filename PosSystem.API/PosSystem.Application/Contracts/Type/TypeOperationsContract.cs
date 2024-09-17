using PosSystem.Application.Contracts.Validations.Type;
using System.ComponentModel.DataAnnotations;

namespace PosSystem.Application.Contracts.Type
{
    public class TypeOperationsContract
    {
        public string Id { get; set; }

        [Required]
        [UniqueTypeNameOnUpdate]
        public string Name { get; set; }

        public string? Note { get; set; }

        [Required]
        [ExistingCompanyId]
        public string CompanyId { get; set; }
    }
}
