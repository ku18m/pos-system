
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PosSystem.Core.Entities
{
    public class Type
    {
        [Key]
        public string TypeId { get; set; }


        public string Name { get; set; }
        public string? Notes { get; set; }


        [ForeignKey("Company")]
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public Type()
        {
            TypeId = Guid.NewGuid().ToString();
        }
    }
}
