using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PosSystem.Core.Entities
{
    public class Client
    {
        [Key]
        public string ClientId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public int Number { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string? Notes { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
        public Client()
        {
            ClientId = Guid.NewGuid().ToString();
        }
    }
}
