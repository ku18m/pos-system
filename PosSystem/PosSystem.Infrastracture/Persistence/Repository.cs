using Microsoft.EntityFrameworkCore;
using PosSystem.Core.Interfaces;
using PosSystem.Infrastracture.Persistence.Data;

namespace PosSystem.Infrastracture.Persistence
{
    public class Repository<Entity> : IRepository<Entity> where Entity : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Entity> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Entity>(); 
        }
        public List<Entity> GetAll()
        {
            return _dbSet.ToList();
        }

        public Entity GetById(string id)
        {
            return _dbSet.Find(id);
        }

        public void Insert(Entity entity)
        {
            _dbSet.Add(entity);
        }
        public void Delete(string id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }
        public void Update(Entity entity)
        {
            _dbSet.Update(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

       
    }
}
