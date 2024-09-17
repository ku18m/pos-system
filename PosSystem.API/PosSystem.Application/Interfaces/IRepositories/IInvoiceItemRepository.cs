using PosSystem.Core.Entities;

namespace PosSystem.Application.Interfaces.IRepositories
{
    public interface IInvoiceItemRepository : IRepository<InvoiceItem>
    {
        Task<IEnumerable<InvoiceItem>> GetInvoiceItemsBySellingPrice(decimal price);
        Task<IEnumerable<InvoiceItem>> GetInvoiceItemsByProductId(string productId);
    }
}
