using PosSystem.Core.Entities;

namespace PosSystem.Core.Interfaces.Repositories
{
    public interface IInvoiceItemRepository : IRepository<InvoiceItem>
    {
        Task<IEnumerable<InvoiceItem>> GetInvoiceItemsBySellingPrice(decimal price);
        Task<IEnumerable<InvoiceItem>> GetInvoiceItemsByProductId(string productId);
    }
}
