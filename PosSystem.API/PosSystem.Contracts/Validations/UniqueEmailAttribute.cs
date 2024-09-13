using System.ComponentModel.DataAnnotations;
using PosSystem.Infrastracture.Persistence.Data;

public class UniqueEmailAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is string email)
        {
            var dbContext = (PosDbContext)validationContext.GetService(typeof(PosDbContext));

            var emailExists = dbContext.Users.Any(u => u.Email == email);

            if (emailExists)
            {
                return new ValidationResult("This email address is already taken.");
            }
        }

        return ValidationResult.Success;
    }
}
