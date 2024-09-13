using System.ComponentModel.DataAnnotations;

namespace PosSystem.Contracts.User
{
    public class UserLoginContract
    {
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
