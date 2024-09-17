using Microsoft.EntityFrameworkCore;
using PosSystem.Application.Interfaces.IRepositories;
using PosSystem.Infrastracture.Persistence.Data;

namespace PosSystem.Infrastracture.Persistence
{
    public class Repository<Entity> : IRepository<Entity> where Entity : class
    {
        protected readonly PosDbContext _context;
        protected readonly DbSet<Entity> _dbSet;

        public Repository(PosDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Entity>();
        }

        public Task<List<Entity>> GetAll()
        {
            return _dbSet.ToListAsync();
        }

        public async Task<Entity> GetById(string id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task Insert(Entity entity)
        {
            _dbSet.AddAsync(entity);
        }
        public async Task Delete(string id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }
        public async Task Update(Entity entity)
        {
            _dbSet.Update(entity);
        }

        public Task<List<Entity>> GetPage(int page, int pageSize)
        {
            return _dbSet.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }
    }
}