using PosSystem.Core.Enums;

namespace PosSystem.Core.Entities
{
    public class User
    {
        public string Id { get; private set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string UserName { get; set; }

        public string Password { get; set; }

        public UserType Role { get; set; } = UserType.Employee;

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }

        public User()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
