using PosSystem.Core.Entities;

namespace PosSystem.Application.Interfaces.IRepositories
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {
        Task<Invoice> GetInvoiceByBillNumber(long number);
        Task<IEnumerable<Invoice>> GetInvoicesByDate(DateTime date);
        Task<IEnumerable<Invoice>> GetInvoicesByDateRange(DateTime start, DateTime end);
        Task<IEnumerable<Invoice>> GetInvoicesByTotalRange(decimal start, decimal end);
        Task<IEnumerable<Invoice>> GetAllDueInvoices();
    }
}
