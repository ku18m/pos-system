using System.ComponentModel.DataAnnotations;

namespace PosSystem.Core.Entities
{
    public class Unit
    {
        [Key]
        public string UnitId { get; set; }

        public string Name { get; set; }
        public string? Notes { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
        public Unit()
        {
            UnitId = Guid.NewGuid().ToString();
        }
    }
}
