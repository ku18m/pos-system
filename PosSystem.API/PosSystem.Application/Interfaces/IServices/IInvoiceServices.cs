 

using PosSystem.Application.Contracts.Invoice;
using PosSystem.Application.Contracts;
using PosSystem.Core.Entities;

namespace PosSystem.Application.Interfaces.IServices
{
    public interface IInvoiceServices
    {
        Task<InvoiceOutContract> AddInvoice(InvoiceCreationContract invoice);
        Task<IEnumerable<InvoiceOutContract>> GetAllInvoices();
        Task DeleteInvoice(string id);

        Task<InvoiceOutContract> UpdateInvoice(string id, InvoiceOperationsContract invoice);
        Task<InvoiceOutContract> GetInvoiceById(string id);
      
        Task<PaginatedOutContract<InvoiceOutContract>> GetInvoicePage(int pageNumber, int pageSize);
        Task<IEnumerable<InvoiceShortOutContract>> GetAllInvoicesShorted();
        Task<int> GetNextInvoiceNumber();
        Task<IEnumerable<Invoice>> GetInvoicesByDateRange(DateTime start, DateTime end);



    }
}
