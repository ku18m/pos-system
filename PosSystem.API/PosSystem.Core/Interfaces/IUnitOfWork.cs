using PosSystem.Core.Entities;
using PosSystem.Core.Interfaces.Repositories;

namespace PosSystem.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository CategoryRepository { get; }
        IClientRepository ClientRepository { get; }
        ICompanyRepository CompanyRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        IInvoiceRepository InvoiceRepository { get; }
        IInvoiceItemRepository InvoiceItemRepository { get; }
        IProductRepository ProductRepository { get; }
        IUnitRepository UnitRepository { get; }
        IUserRepository UserRepository { get; }
        
        Task<int> Save();
    }
}
