using PosSystem.Application.Interfaces.IRepositories;
using PosSystem.Application.Interfaces.IServices;
using PosSystem.Infrastracture.Persistence.Data;
using PosSystem.Infrastracture.Persistence.Repositories;

namespace PosSystem.Infrastracture.Persistence
{
    class UnitOfWork(PosDbContext context) : IUnitOfWork
    {
        #region Private Members
        ICategoryRepository? _categoryRepository;

        IClientRepository? _clientRepository;

        ICompanyRepository? _companyRepository;

        IInvoiceRepository? _invoiceRepository;

        IInvoiceItemRepository? _invoiceItemRepository;

        IProductRepository? _productRepository;

        IUnitRepository? _unitRepository;

        IUserRepository? _userRepository;
        #endregion

        #region Repositories Getters
        public ICategoryRepository CategoryRepository
        {
            get
            {
                _categoryRepository ??= new CategoryRepository(context);

                return _categoryRepository;
            }
        }

        public IClientRepository ClientRepository
        {
            get
            {
                _clientRepository ??= new ClientRepository(context);

                return _clientRepository;
            }
        }

        public ICompanyRepository CompanyRepository
        {
            get
            {
                _companyRepository ??= new CompanyRepository(context);

                return _companyRepository;
            }
        }

        public IInvoiceRepository InvoiceRepository
        {
            get
            {
                _invoiceRepository ??= new InvoiceRepository(context);

                return _invoiceRepository;
            }
        }

        public IInvoiceItemRepository InvoiceItemRepository
        {
            get
            {
                _invoiceItemRepository ??= new InvoiceItemRepository(context);

                return _invoiceItemRepository;
            }
        }

        public IProductRepository ProductRepository
        {
            get
            {
                _productRepository ??= new ProductRepository(context);

                return _productRepository;
            }
        }

        public IUnitRepository UnitRepository
        {
            get
            {
                _unitRepository ??= new UnitRepository(context);

                return _unitRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                _userRepository ??= new UserRepository(context);

                return _userRepository;
            }
        }
        #endregion

        public void Dispose()
        {
            context.Dispose();
        }

        public async Task<int> Save()
        {
            return await context.SaveChangesAsync();
        }
    }
}