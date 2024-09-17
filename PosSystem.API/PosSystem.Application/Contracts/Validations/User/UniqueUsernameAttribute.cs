using PosSystem.Application.Interfaces.IRepositories;
using System.ComponentModel.DataAnnotations;

namespace PosSystem.Application.Contracts.Validations.User
{
    public class UniqueUsernameAttribute : ValidationAttribute
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

                var user = userRepo.GetUserByUserNameAsync(username).Result;

                if (user != null)
                {
                    return new ValidationResult("This username address is already taken.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
