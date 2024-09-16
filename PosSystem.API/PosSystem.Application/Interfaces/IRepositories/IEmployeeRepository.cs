using PosSystem.Core.Entities;

namespace PosSystem.Application.Interfaces.IRepositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetEmployeesByName(string name);
        Task<IEnumerable<Employee>> GetEmployeesByPhone(string phone);
        Task<IEnumerable<Employee>> GetEmployeesByAddress(string address);
    }
}
