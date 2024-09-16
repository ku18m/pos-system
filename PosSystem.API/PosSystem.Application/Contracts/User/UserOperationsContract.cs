using System.ComponentModel.DataAnnotations;

namespace PosSystem.Application.Contracts.User
{
    public class UserOperationsContract
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? Password { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [UniqueEmail]
        public string Email { get; set; }

        public string Role { get; set; }
    }
}
