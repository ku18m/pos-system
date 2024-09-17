using System.ComponentModel.DataAnnotations;

namespace PosSystem.Application.Contracts.User
{
    public class UserLoginContract
    {
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
