using PosSystem.Application.Contracts.Client;

namespace PosSystem.Application.Interfaces.IServices
{
    public interface IClientServices
    {
        Task<ClientOutContract> Add(ClientOperationsContract client);
        Task<List<ClientOutContract>> GetAll();
        Task DeleteById(string id);
        Task Edit(string id, ClientOperationsContract client);
        Task<ClientOutContract> GetById(string id);
        Task<ClientOutContract> GetByName(string name);
        Task<IEnumerable<ClientOutContract>> GetClientsByName(string name);
        Task<ClientOutContract> GetByPhone(string phone);
        Task<IEnumerable<ClientOutContract>> GetClientsByAddress(string address);
    }
}
