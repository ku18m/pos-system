using PosSystem.Application.Contracts.Client;
using PosSystem.Application.Interfaces.IRepositories;
using System.ComponentModel.DataAnnotations;

namespace PosSystem.Application.Contracts.Validations.Client
{
    public class UniqueClientNameOnUpdateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string name)
            {
                var clientRepo = validationContext.GetService(typeof(IClientRepository)) as IClientRepository;

                if (clientRepo == null)
                {
                    throw new InvalidOperationException("IClientRepository service is not available.");
                }

                var contract = (ClientOperationsContract)validationContext.ObjectInstance;

                var client = clientRepo.GetClientByName(name).Result;

                if (client != null && client.ClientId != contract.Id)
                {
                    return new ValidationResult("This client name is already taken.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
