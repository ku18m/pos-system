using System;
using System.ComponentModel.DataAnnotations;

namespace PosSystem.Core.Entities
{
    public class Employee
    {
        [Key]
        public string EmployeeId { get; set; }


        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public DateTime DateOfBirth { get; set; }

        public string? PhoneNumber { get; set; }

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
