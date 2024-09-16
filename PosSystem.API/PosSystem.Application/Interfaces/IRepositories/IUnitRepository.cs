using PosSystem.Core.Entities;

namespace PosSystem.Application.Interfaces.IRepositories
{
    public interface IUnitRepository : IRepository<Unit>
    {
        Task<Unit> GetUnitByName(string name);
        Task<IEnumerable<Unit>> GetUnitsByName(string name);
    }
}
