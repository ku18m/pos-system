using System.ComponentModel.DataAnnotations;


namespace PosSystem.Core.Entities
{
    public class Client
    {
        [Key]
        public string ClientId { get; set; }

        public string Name { get; set; }

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
