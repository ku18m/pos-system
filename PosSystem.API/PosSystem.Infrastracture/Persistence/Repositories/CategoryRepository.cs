using Microsoft.EntityFrameworkCore;
using PosSystem.Application.Interfaces.IRepositories;
using PosSystem.Core.Entities;
using PosSystem.Infrastracture.Persistence.Data;

namespace PosSystem.Infrastracture.Persistence.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(PosDbContext context) : base(context) {}

        public async Task<IEnumerable<Category>> GetCategoriesByCompanyId(string companyId)
        {
            return await _dbSet.Where(c => c.CompanyId == companyId).ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetCategoriesByName(string name)
        {
            return await _dbSet.Where(c => c.Name.Contains(name)).ToListAsync();
        }

        public async Task<Category?> GetCategoryByName(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.Name == name);
        }
    }
}
