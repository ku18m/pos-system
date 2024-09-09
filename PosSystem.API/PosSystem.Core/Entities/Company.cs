using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PosSystem.Core.Entities
{
    public class Company
    {
        [Key]
        public string CompanyId { get; set; }


        [Required(ErrorMessage = "Company Name is required")]
        public string Name { get; set; }

        public string? Notes { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<Type> Types { get; set; }
        public Company()
        {
            CompanyId = Guid.NewGuid().ToString();
        }
    }
}
