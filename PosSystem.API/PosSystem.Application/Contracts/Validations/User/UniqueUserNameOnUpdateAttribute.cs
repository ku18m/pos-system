using PosSystem.Application.Contracts.User;
using PosSystem.Application.Interfaces.IRepositories;
using System.ComponentModel.DataAnnotations;

namespace PosSystem.Application.Contracts.Validations.User
{
    public class UniqueUserNameOnUpdateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string username)
            {
                var userRepo = validationContext.GetService(typeof(IUserRepository)) as IUserRepository;

                if (userRepo == null)
                {
                    throw new InvalidOperationException("IUserRepository service is not available.");
                }

                var contract = (UserOperationsContract)validationContext.ObjectInstance;

                var user = userRepo.GetUserByUserNameAsync(username).Result;

                if (user != null && user.Id != contract.Id)
                {
                    return new ValidationResult("This username address is already taken.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
