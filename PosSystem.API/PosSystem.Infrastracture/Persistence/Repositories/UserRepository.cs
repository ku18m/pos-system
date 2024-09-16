using Microsoft.EntityFrameworkCore;
using PosSystem.Core.Entities;
using PosSystem.Core.Interfaces.Repositories;
using PosSystem.Infrastracture.Persistence.Data;

namespace PosSystem.Infrastracture.Persistence.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(PosDbContext context) : base(context) { }

        public Task<User> GetUserByEmailAsync(string email)
        {
            return _dbSet.Where(u => u.Email == email).FirstOrDefaultAsync()!;
        }
    }
}
