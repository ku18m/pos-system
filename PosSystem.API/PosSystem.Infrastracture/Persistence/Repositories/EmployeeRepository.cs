using Microsoft.EntityFrameworkCore;
using PosSystem.Application.Interfaces.IRepositories;
using PosSystem.Core.Entities;
using PosSystem.Infrastracture.Persistence.Data;

namespace PosSystem.Infrastracture.Persistence.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(PosDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByName(string name)
        {
            return await _dbSet.Where(e => e.FullName.Contains(name)).ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByPhone(string phone)
        {
            return await _dbSet.Where(e => e.PhoneNumber.Contains(phone)).ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByAddress(string address)
        {
            return await _dbSet.Where(e => e.Address.Contains(address)).ToListAsync();
        }
    }
}
