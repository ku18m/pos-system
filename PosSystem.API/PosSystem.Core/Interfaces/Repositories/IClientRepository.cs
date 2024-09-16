using PosSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Core.Interfaces.Repositories
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<Client> GetClientByName(string name);
        Task<Client> GetClientByPhone(string phone);
        Task<IEnumerable<Client>> GetClientsByAddress(string address);
        Task<IEnumerable<Client>> GetClientsByPhone(string phone);
        Task<IEnumerable<Client>> GetClientsByName(string name);
    }
}
