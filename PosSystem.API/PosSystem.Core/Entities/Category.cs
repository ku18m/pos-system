
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PosSystem.Core.Entities
{
    public class Category
    {
        [Key]
        public string CategoryId { get; set; }


        public string Name { get; set; }
        public string? Notes { get; set; }


        [ForeignKey("Company")]
        public string CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public Category()
        {
            CategoryId = Guid.NewGuid().ToString();
        }
    }
}
