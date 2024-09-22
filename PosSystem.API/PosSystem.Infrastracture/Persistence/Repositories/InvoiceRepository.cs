using Microsoft.EntityFrameworkCore;
using PosSystem.Application.Interfaces.IRepositories;
using PosSystem.Core.Entities;
using PosSystem.Infrastracture.Persistence.Data;
using PosSystem.Infrastracture.Persistence.Helpers;

namespace PosSystem.Infrastracture.Persistence.Repositories
{
    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(PosDbContext context) : base(context)
        {
        }

        public Task<Invoice> GetInvoiceByBillNumber(long number)
        {
            return _dbSet.FirstOrDefaultAsync(i => i.BillNumber == number);
        }

        public async Task<IEnumerable<Invoice>> GetInvoicesByDate(DateTime date)
        {
            return await _dbSet.Where(i => i.Date == date).ToListAsync();
        }

        public async Task<IEnumerable<Invoice>> GetInvoicesByTotalRange(decimal start, decimal end)
        {
            return await _dbSet.Where(i => i.TotalAmount > start && i.TotalAmount < end).ToListAsync();
        }
        
        public async Task<IEnumerable<Invoice>> GetInvoicesByDateRange(DateTime start, DateTime end)
        {
            return await _dbSet.Where(i => i.Date > start && i.Date < end).ToListAsync();
        }

        public async Task<IEnumerable<Invoice>> GetAllDueInvoices()
        {
            return await _dbSet.Where(i => i.DueAmount > 0).ToListAsync();
        }
        public async Task<int> GetNextInvoiceNumber()
        {
            var sqlQuery = @"SELECT current_value AS CurrentValue FROM sys.sequences WHERE name = 'BillNumber'";
            var currentValue = await _context.Database.SqlQueryRaw<SequenceValue>(sqlQuery).FirstAsync();

            return currentValue.CurrentValue + 1;
        }
    }
}
