using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosSystem.Core.Entities
{
    public class Client
    {
        [Key]
        public string ClientId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Number { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression(@"^\d{14}$", ErrorMessage = "Phone number must be exactly 14 digits.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        public string? Notes { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public Client()
        {
            ClientId = Guid.NewGuid().ToString();
        }
    }
}
