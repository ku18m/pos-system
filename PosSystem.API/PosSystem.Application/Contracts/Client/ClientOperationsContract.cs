using PosSystem.Application.Contracts.Validations.Client;
using System.ComponentModel.DataAnnotations;

namespace PosSystem.Application.Contracts.Client
{
    public class ClientOperationsContract
    {
        public string Id { get; set; }

        [Required]
        [UniqueClientNameOnUpdate]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [Phone(ErrorMessage = "Invalid phone number.")]
        [Length(14, 14, ErrorMessage = "Phone number must have 14 digits.")]
        public string PhoneNumber { get; set; }
    }
}
