using PosSystem.Core.Entities;
using PosSystem.Core.Interfaces.Repositories;
using PosSystem.Infrastracture.Persistence.Data;

namespace PosSystem.Infrastracture.Persistence.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(PosDbContext context) : base(context) {}

        public async Task<Category> GetCategoryByName(string name)
        {
            return _dbSet.Where(c => c.Name == name).FirstOrDefault();
        }
    }
}
