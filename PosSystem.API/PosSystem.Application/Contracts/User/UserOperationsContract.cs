using PosSystem.Application.Contracts.Validations.User;
using System.ComponentModel.DataAnnotations;

namespace PosSystem.Application.Contracts.User
{
    public class UserOperationsContract
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? Password { get; set; }

        [UniqueUserNameOnUpdate(ErrorMessage = "Username already exists.")]
        public string Username { get; set; }


        [AllowedValues("Employee", "Admin", ErrorMessage = "Invalid user role.")]
        public string Role { get; set; }


        [DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; }


        [DataType(DataType.Time)]
        public TimeSpan EndTime { get; set; }
    }
}
