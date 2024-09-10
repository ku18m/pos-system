using System.ComponentModel.DataAnnotations;

namespace PosSystem.Core.Entities
{
    public class Client
    {
        [Key]
        public string ClientId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Number { get; set; }

        public string Phone { get; set; }

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
