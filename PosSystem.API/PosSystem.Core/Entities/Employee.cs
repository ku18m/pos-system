using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosSystem.Core.Entities
{
    public class Employee
    {
        [Key]
        public string EmployeeId { get; set; }


        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public int? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }

        
        public virtual ICollection<Client> Clients { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public Employee()
        {
            EmployeeId = Guid.NewGuid().ToString();
        }
    }
}
