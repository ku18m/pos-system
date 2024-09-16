using PosSystem.Core.Entities;

namespace PosSystem.Core.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByEmailAsync(string email);
    }
}
