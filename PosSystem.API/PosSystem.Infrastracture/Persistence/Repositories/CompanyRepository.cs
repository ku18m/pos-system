using Microsoft.EntityFrameworkCore;
using PosSystem.Application.Interfaces.IRepositories;
using PosSystem.Core.Entities;
using PosSystem.Infrastracture.Persistence.Data;

namespace PosSystem.Infrastracture.Persistence.Repositories
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(PosDbContext context) : base(context) { }

        public async Task<Company> GetCompanyByName(string name)
        {
            return _dbSet.Where(c => c.Name == name).FirstOrDefault();
        }

        public async Task<IEnumerable<Company>> GetCompaniesByName(string name)
        {
            return await _dbSet.Where(c => c.Name == name).ToListAsync();
        }
    }
}
