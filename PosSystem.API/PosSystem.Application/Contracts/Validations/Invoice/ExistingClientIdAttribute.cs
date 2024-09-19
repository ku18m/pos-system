using PosSystem.Application.Interfaces.IRepositories;
using System.ComponentModel.DataAnnotations;

namespace PosSystem.Application.Contracts.Validations.Invoice
{
    public class ExistingClientIdAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var clientId = value as string;

            if (clientId == null)
            {
                return new ValidationResult("Client Id is required");
            }

            var clientRepo = validationContext.GetService(typeof(IClientRepository)) as IClientRepository;

            if (clientRepo == null)
            {
                return new ValidationResult("Client Repository not found");
            }

            var client = clientRepo.GetById(clientId).Result;

            if (client == null)
            {
                return new ValidationResult("Client not found");
            }

            return ValidationResult.Success;
        }
    }
}
