using PosSystem.Application.Contracts;
using PosSystem.Application.Contracts.Company;

namespace PosSystem.Application.Interfaces.IServices
{
    public interface ICompanyServices
    {
        Task<CompanyOutContract> Add(CompanyCreationContract company);
        Task<List<CompanyOutContract>> GetAll();
        Task Delete(string id);
        Task Edit(string id, CompanyOperationsContract company);
        Task<CompanyOutContract> GetById(string id);
        Task<CompanyOutContract> GetByName(string name);
        Task<IEnumerable<CompanyOutContract>> GetCompaniesByName(string name);
        Task<PaginatedOutContract<CompanyOutContract>> GetCompanyPage(int pageNumber, int pageSize);
        Task<IEnumerable<CompanyShortOutContract>> GetAllShorted();
    }
}
