using Microsoft.EntityFrameworkCore;
using PosSystem.Application.Interfaces.IRepositories;
using PosSystem.Core.Entities;
using PosSystem.Infrastracture.Persistence.Data;

namespace PosSystem.Infrastracture.Persistence.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(PosDbContext context) : base(context) { }

        public Task<User> GetUserByUserNameAsync(string username)
        {
            return _dbSet.Where(u => u.UserName == username).FirstOrDefaultAsync()!;
        }
    }
}
