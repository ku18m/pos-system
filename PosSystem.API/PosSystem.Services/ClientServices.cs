using AutoMapper;
using PosSystem.Contracts.Client;
using PosSystem.Core.Entities;
using PosSystem.Core.Interfaces;


namespace PosSystem.Services
{
    public class ClientServices<TClientIn, TClientOut>(IUnitOfWork unitOfWork, IMapper mapper) : IClientService<AddClientContract, ReturnClientContract>
    {
        public async Task<ReturnClientContract> Add(AddClientContract client)
        {
            if (client == null)
                throw new Exception("Client information must be provided");

            var existingClient = await unitOfWork.ClientRepository.GetClientByName(client.Name);
            if (existingClient != null)
                throw new Exception("Client name already exists ");

            existingClient = await unitOfWork.ClientRepository.GetClientByPhone(client.PhoneNumber);
            if (existingClient != null)
                throw new Exception("A client with this phone number already exists.");

            var newClient = mapper.Map<Client>(client);
            await unitOfWork.ClientRepository.Insert(newClient);
            await unitOfWork.Save();

            return mapper.Map<ReturnClientContract>(newClient);
        }
        public async Task<List<ReturnClientContract>> GetAll()
        {
            var clients = await unitOfWork.ClientRepository.GetAll();
            return mapper.Map<List<ReturnClientContract>>(clients);
        }
        public async Task DeleteById(string id)
        {
            var client = await unitOfWork.ClientRepository.GetById(id);
            if (client == null)
                throw new Exception("Client not found.");

            await unitOfWork.ClientRepository.Delete(id);
            await unitOfWork.Save();
        }
        public async Task Edit(string id, AddClientContract client)
        {
            var existingClient = await unitOfWork.ClientRepository.GetById(id);
            if (existingClient == null)
                throw new Exception("Client not found.");

            mapper.Map(client, existingClient);
            await unitOfWork.ClientRepository.Update(existingClient);
            await unitOfWork.Save();
        }

        public async Task<ReturnClientContract> GetById(string id)
        {
            var client = await unitOfWork.ClientRepository.GetById(id);
            if (client == null)
                throw new Exception("Client not found.");

            return mapper.Map<ReturnClientContract>(client);
        }
        public async Task<ReturnClientContract> GetByName(string name)
        {
            var client = await unitOfWork.ClientRepository.GetClientByName(name);
            if (client == null)
                throw new Exception("Client not found.");

            return mapper.Map<ReturnClientContract>(client);
        }
        public async Task<IEnumerable<ReturnClientContract>> GetClientsByName(string name)
        {
            var clients = await unitOfWork.ClientRepository.GetClientsByName(name);
            if (!clients.Any())
                throw new Exception("No clients found with this name.");

            return mapper.Map<IEnumerable<ReturnClientContract>>(clients);
        }
        public async Task<ReturnClientContract> GetByPhone(string phone)
        {
            var client = await unitOfWork.ClientRepository.GetClientByPhone(phone);
            if (client == null)
                throw new Exception("Client not found.");

            return mapper.Map<ReturnClientContract>(client);
        }
        public async Task<IEnumerable<ReturnClientContract>> GetClientsByAddress(string address)
        {
            var clients = await unitOfWork.ClientRepository.GetClientsByAddress(address);
            if (!clients.Any())
                throw new Exception("No clients found with this address.");

            return mapper.Map<IEnumerable<ReturnClientContract>>(clients);
        }
    }
}
