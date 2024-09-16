using PosSystem.Application.Interfaces.IRepositories;

namespace PosSystem.Application.Interfaces.IServices
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
