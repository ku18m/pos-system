using Microsoft.EntityFrameworkCore;
using PosSystem.Application.Interfaces.IRepositories;
using PosSystem.Core.Entities;
using PosSystem.Infrastracture.Persistence.Data;

namespace PosSystem.Infrastracture.Persistence.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(PosDbContext context) : base(context) { }

        public Task<Product> GetProductByName(string name)
        {
            return _dbSet.Where(p => p.Name == name).FirstOrDefaultAsync()!;
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string categoryId)
        {
            return await _dbSet.Where(p => p.CategoryId == categoryId).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCompany(string companyId)
        {
            return await _dbSet.Where(p => p.CompanyId == companyId).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            return await _dbSet.Where(p => p.Name.Contains(name)).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByUnit(string unitId)
        {
            return await _dbSet.Where(p => p.UnitId == unitId).ToListAsync();
        }
    }
}
