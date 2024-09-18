using Microsoft.EntityFrameworkCore;
using PosSystem.Application.Interfaces.IRepositories;
using PosSystem.Core.Entities;
using PosSystem.Infrastracture.Persistence.Data;
using PosSystem.Infrastracture.Persistence.Helpers;

namespace PosSystem.Infrastracture.Persistence.Repositories
{
    public class InvoiceItemRepository : Repository<InvoiceItem>, IInvoiceItemRepository
    {
        public InvoiceItemRepository(PosDbContext context) : base(context) { }

        public async Task<IEnumerable<InvoiceItem>> GetInvoiceItemsBySellingPrice(decimal price)
        {
            return await _dbSet.Where(i => i.Price == price).ToListAsync();
        }

        public async Task<IEnumerable<InvoiceItem>> GetInvoiceItemsBySellingPriceRange(decimal start, decimal end)
        {
            return await _dbSet.Where(i => i.Price > start && i.Price < end).ToListAsync();
        }


        public async Task<IEnumerable<InvoiceItem>> GetInvoiceItemsByProductId(string productId)
        {
            return await _dbSet.Where(i => i.ProductId == productId).ToListAsync();
        }
    
    }
}
