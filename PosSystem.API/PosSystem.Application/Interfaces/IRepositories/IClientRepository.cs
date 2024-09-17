using PosSystem.Core.Entities;

namespace PosSystem.Application.Interfaces.IRepositories
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<Client?> GetClientByName(string name);
        Task<Client?> GetClientByPhone(string phone);
        Task<IEnumerable<Client>> GetClientsByAddress(string address);
        Task<IEnumerable<Client>> GetClientsByPhone(string phone);
        Task<IEnumerable<Client>> GetClientsByName(string name);
        Task<int> GetNextClientNumber();
    }
}
