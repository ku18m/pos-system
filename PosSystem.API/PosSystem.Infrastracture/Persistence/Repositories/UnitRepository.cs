using Microsoft.EntityFrameworkCore;
using PosSystem.Core.Entities;
using PosSystem.Core.Interfaces.Repositories;
using PosSystem.Infrastracture.Persistence.Data;

namespace PosSystem.Infrastracture.Persistence.Repositories
{
    public class UnitRepository : Repository<Unit>, IUnitRepository
    {
        public UnitRepository(PosDbContext context) : base(context) { }

        public Task<Unit> GetUnitByName(string name)
        {
            return _dbSet.Where(u => u.Name == name).FirstOrDefaultAsync()!;
        }

        public async Task<IEnumerable<Unit>> GetUnitsByName(string name)
        {
            return await _dbSet.Where(u => u.Name == name).ToListAsync();
        }
    }
}
