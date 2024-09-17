using PosSystem.Application.Contracts;
using PosSystem.Application.Contracts.Type;

namespace PosSystem.Application.Interfaces.IServices
{
    public interface ITypeServices
    {
        Task<TypeOutContract> Add(TypeCreationContract unit);
        Task<List<TypeOutContract>> GetAll();
        Task Delete(string id);
        Task Edit(string id, TypeOperationsContract unit);
        Task<TypeOutContract> GetById(string id);
        Task<IEnumerable<TypeOutContract>> GetTypesByName(string name);
        Task<IEnumerable<TypeOutContract>> GetByCompanyId(string companyId);
        Task<PaginatedOutContract<TypeOutContract>> GetTypePage(int page, int pageSize);
        Task<IEnumerable<TypeShortOutContract>> GetAllShorted();
    }
}
