using AutoMapper;
using PosSystem.Application.Contracts;
using PosSystem.Application.Contracts.Client;
using PosSystem.Application.Interfaces.IServices;
using PosSystem.Core.Entities;


namespace PosSystem.Application.Services
{
    public class ClientServices(IUnitOfWork unitOfWork, IMapper mapper) : IClientServices
    {
        public async Task<ClientOutContract> Add(ClientCreationContract client)
        {
            if (client == null)
                throw new Exception("Client information must be provided");

            var newClient = mapper.Map<Client>(client);

            await unitOfWork.ClientRepository.Insert(newClient);

            try
            {
                await unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured while creating the client");
            }

            return mapper.Map<ClientOutContract>(newClient);
        }

        public async Task<List<ClientOutContract>> GetAll()
        {
            var clients = await unitOfWork.ClientRepository.GetAll();
            return mapper.Map<List<ClientOutContract>>(clients);
        }
        public async Task DeleteById(string id)
        {
            var client = await unitOfWork.ClientRepository.GetById(id);
            if (client == null)
                throw new Exception("Client not found.");

            await unitOfWork.ClientRepository.Delete(id);
            await unitOfWork.Save();
        }
        public async Task Edit(string id, ClientOperationsContract client)
        {
            var existingClient = await unitOfWork.ClientRepository.GetById(id);

            if (existingClient == null)
                throw new Exception("Client not found.");

            mapper.Map(client, existingClient);

            await unitOfWork.ClientRepository.Update(existingClient);

            await unitOfWork.Save();
        }

        public async Task<ClientOutContract> GetById(string id)
        {
            var client = await unitOfWork.ClientRepository.GetById(id);
            if (client == null)
                throw new Exception("Client not found.");

            return mapper.Map<ClientOutContract>(client);
        }
        public async Task<ClientOutContract> GetByName(string name)
        {
            var client = await unitOfWork.ClientRepository.GetClientByName(name);
            if (client == null)
                throw new Exception("Client not found.");

            return mapper.Map<ClientOutContract>(client);
        }
        public async Task<IEnumerable<ClientOutContract>> GetClientsByName(string name)
        {
            var clients = await unitOfWork.ClientRepository.GetClientsByName(name);
            if (!clients.Any())
                throw new Exception("No clients found with this name.");

            return mapper.Map<IEnumerable<ClientOutContract>>(clients);
        }
        public async Task<ClientOutContract> GetByPhone(string phone)
        {
            var client = await unitOfWork.ClientRepository.GetClientByPhone(phone);
            if (client == null)
                throw new Exception("Client not found.");

            return mapper.Map<ClientOutContract>(client);
        }
        public async Task<IEnumerable<ClientOutContract>> GetClientsByAddress(string address)
        {
            var clients = await unitOfWork.ClientRepository.GetClientsByAddress(address);

            if (!clients.Any())
                throw new Exception("No clients found with this address.");

            return mapper.Map<IEnumerable<ClientOutContract>>(clients);
        }

        public async Task<PaginatedOutContract<ClientOutContract>> GetClientPage(int pageNumber, int pageSize)
        {
            var totalPages = await unitOfWork.ClientRepository.GetTotalPages(pageSize);

            if (pageNumber > totalPages)
                throw new Exception("Page number is greater than total pages.");

            var clients = await unitOfWork.ClientRepository.GetPage(pageNumber, pageSize);

            return new PaginatedOutContract<ClientOutContract>
            {
                Data = mapper.Map<IEnumerable<ClientOutContract>>(clients),
                TotalPages = totalPages,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<IEnumerable<ClientShortOutContract>> GetAllShorted()
        {
            var clients = await unitOfWork.ClientRepository.GetAll();

            return mapper.Map<IEnumerable<ClientShortOutContract>>(clients);
        }

        

        public async Task<int> GetNextClientNumber()
        {
            return await unitOfWork.ClientRepository.GetNextClientNumber();
        }
    }
}
