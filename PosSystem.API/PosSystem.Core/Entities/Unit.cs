using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PosSystem.Core.Entities
{
    public class Unit
    {
        [Key]
        public string UnitId { get; set; }

        [Required(ErrorMessage = "Unit is required")]
        public string Name { get; set; }
        public string? Notes { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public Unit()
        {
            UnitId = Guid.NewGuid().ToString();
        }
    }
}
