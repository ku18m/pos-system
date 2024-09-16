using PosSystem.Core.Entities;

namespace PosSystem.Core.Interfaces.Repositories
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<Company> GetCompanyByName(string name);
        Task<IEnumerable<Company>> GetCompaniesByName(string name);
    }
}
