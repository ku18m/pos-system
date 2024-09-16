using PosSystem.Core.Entities;

namespace PosSystem.Core.Interfaces.Repositories
{
    public interface IUnitRepository : IRepository<Unit>
    {
        Task<Unit> GetUnitByName(string name);
        Task<IEnumerable<Unit>> GetUnitsByName(string name);
    }
}
