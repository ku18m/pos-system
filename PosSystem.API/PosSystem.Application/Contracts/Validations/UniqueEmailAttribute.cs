using PosSystem.Application.Interfaces.IRepositories;
using System.ComponentModel.DataAnnotations;

public class UniqueEmailAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is string email)
        {
            var userRepo = validationContext.GetService(typeof(IUserRepository)) as IUserRepository;

            if (userRepo == null)
            {
                throw new InvalidOperationException("IUserRepository service is not available.");
            }

            var user = userRepo.GetUserByEmailAsync(email).Result;

            if (user != null)
            {
                return new ValidationResult("This email address is already taken.");
            }
        }

        return ValidationResult.Success;
    }
}
