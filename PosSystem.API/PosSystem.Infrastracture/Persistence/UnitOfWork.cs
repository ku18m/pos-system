using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PosSystem.Application.Interfaces.IRepositories;
using PosSystem.Application.Interfaces.IServices;
using PosSystem.Infrastracture.Persistence.Data;
using PosSystem.Infrastracture.Persistence.Repositories;

namespace PosSystem.Infrastracture.Persistence
{
    class UnitOfWork(PosDbContext context) : IUnitOfWork
    {
        private IDbContextTransaction? _transaction;

        #region Private Repositories

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

        #region Transaction Management

        public async Task BeginTransactionAsync()
        {
            _transaction = await context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();

                await _transaction.DisposeAsync();

                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();

                await _transaction.DisposeAsync();

                _transaction = null;
            }
        }

        #endregion

        public void Dispose()
        {
            context.Dispose();

            _transaction?.Dispose();
        }

        public async Task<int> Save()
        {
            int result;

            try
            {
                result = await context.SaveChangesAsync();

                if (_transaction != null)
                {
                    await CommitTransactionAsync();
                }
            }
            catch (Exception)
            {
                if (_transaction != null)
                {
                    await RollbackTransactionAsync();
                }

                throw;
            }


            return result;
        }
    }
}