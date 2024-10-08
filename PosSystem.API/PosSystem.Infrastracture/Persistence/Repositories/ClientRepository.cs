﻿using Microsoft.EntityFrameworkCore;
using PosSystem.Application.Interfaces.IRepositories;
using PosSystem.Core.Entities;
using PosSystem.Infrastracture.Persistence.Data;
using PosSystem.Infrastracture.Persistence.Helpers;

namespace PosSystem.Infrastracture.Persistence.Repositories
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(PosDbContext context) : base(context)
        {
        }

        public async Task<Client?> GetClientByName(string name)
        {
            return _dbSet.Where(c => c.Name == name).FirstOrDefault();
        }

        public async Task<Client?> GetClientByPhone(string phone)
        {
            return _dbSet.Where(c => c.Phone == phone).FirstOrDefault();
        }

        public async Task<IEnumerable<Client>> GetClientsByAddress(string address)
        {
            return await _dbSet.Where(c => c.Address.Contains(address)).ToListAsync();
        }

        public async Task<IEnumerable<Client>> GetClientsByName(string name)
        {
            return await _dbSet.Where(c => c.Name.Contains(name)).ToListAsync();
        }

        public async Task<IEnumerable<Client>> GetClientsByPhone(string phone)
        {
            return await _dbSet.Where(c => c.Phone.Contains(phone)).ToListAsync();
        }

        public async Task<int> GetNextClientNumber()
        {
            var sqlQuery = @"SELECT current_value AS CurrentValue FROM sys.sequences WHERE name = 'ClientNumber'";
            var currentValue = await _context.Database.SqlQueryRaw<SequenceValue>(sqlQuery).FirstAsync();

            return currentValue.CurrentValue + 1;
        }
    }
}
