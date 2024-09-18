using PosSystem.Application.Contracts;
using PosSystem.Application.Contracts.Unit;

namespace PosSystem.Application.Interfaces.IServices
{
    public interface IUnitServices
    {
        Task<UnitOutContract> Add(UnitCreationContract unit);
        Task<List<UnitOutContract>> GetAll();
        Task Delete(string id);
        Task Edit(string id, UnitOperationsContract unit);
        Task<UnitOutContract> GetById(string id);
        Task<List<UnitOutContract>> GetUnitsByName(string name);
        Task<PaginatedOutContract<UnitOutContract>> GetPage(int page, int pageSize);
        Task<IEnumerable<UnitShortOutContract>> GetAllShorted();
    }
}
