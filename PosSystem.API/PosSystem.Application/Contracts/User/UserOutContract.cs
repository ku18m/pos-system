namespace PosSystem.Application.Contracts.User
{
    public class UserOutContract
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Role { get; set; }
    }
}
