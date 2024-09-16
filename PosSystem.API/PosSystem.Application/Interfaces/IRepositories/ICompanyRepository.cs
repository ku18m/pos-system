using PosSystem.Core.Entities;

namespace PosSystem.Application.Interfaces.IRepositories
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<Company> GetCompanyByName(string name);
        Task<IEnumerable<Company>> GetCompaniesByName(string name);
    }
}
