using AutoMapper;
using PosSystem.Application.Contracts;
using PosSystem.Application.Contracts.Invoice;
using PosSystem.Application.Interfaces.IServices;
using PosSystem.Core.Entities;


namespace PosSystem.Application.Services
{
    public class InvoiceServices(IUnitOfWork unitOfWork, IMapper mapper) : IInvoiceServices
    {
        public async Task<InvoiceOutContract> AddInvoice(InvoiceCreationContract invoice)
        {
            await unitOfWork.BeginTransactionAsync();

            try
            {
                var newInvoice = mapper.Map<Invoice>(invoice);

                foreach(var item in newInvoice.Items)
                {
                    var product = await unitOfWork.ProductRepository.GetById(item.ProductId) ?? throw new Exception("Product not found.");

                    product.Quantity -= item.Quantity;
                }

                await unitOfWork.InvoiceRepository.Insert(newInvoice);

                await unitOfWork.Save();

                var user = await unitOfWork.UserRepository.GetById(newInvoice.UserId);

                newInvoice.User = user;

                return mapper.Map<InvoiceOutContract>(newInvoice);
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync();
                throw new Exception("An error occured while creating the invoice", ex);
            }
        }

        public async Task<IEnumerable<InvoiceOutContract>> GetAllInvoices()
        {
            var Invoices = await unitOfWork.InvoiceRepository.GetAll();

            return mapper.Map<IEnumerable<InvoiceOutContract>>(Invoices);
        }

        public async Task<IEnumerable<InvoiceShortOutContract>> GetAllInvoicesShorted()
        {
            var Invoices = await unitOfWork.InvoiceRepository.GetAll();

            return mapper.Map<IEnumerable<InvoiceShortOutContract>>(Invoices);
        }

        public async Task DeleteInvoice(string id)
        {
            var invoice = await unitOfWork.InvoiceRepository.GetById(id) ?? throw new Exception("invoice not found.");

            await unitOfWork.BeginTransactionAsync();

            try
            {
                foreach (var item in invoice.Items)
                {
                    item.Product.Quantity += item.Quantity;
                }

                await unitOfWork.InvoiceRepository.Delete(id);

                await unitOfWork.Save();
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync();
                throw new Exception("An error occured while deleting the invoice", ex);
            }
        }

        public async Task<InvoiceOutContract> GetInvoiceById(string id)
        {
            var invoice = await unitOfWork.InvoiceRepository.GetById(id);

            if (invoice == null)
                throw new Exception("invoice not found.");

            return mapper.Map<InvoiceOutContract>(invoice);
        }

        public async Task<PaginatedOutContract<InvoiceOutContract>> GetInvoicesPage(int pageNumber, int pageSize)
        {
            var totalPages = await unitOfWork.InvoiceRepository.GetTotalPages(pageSize);

            if (pageNumber > totalPages)
                throw new Exception("Page number is greater than total pages.");

            var invoices = await unitOfWork.InvoiceRepository.GetPage(pageNumber, pageSize);

            return new PaginatedOutContract<InvoiceOutContract>
            {
                Data = mapper.Map<IEnumerable<InvoiceOutContract>>(invoices),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }
        public async Task<IEnumerable<InvoiceOutContract>> GetInvoicesByDateRange(DateTime startDate, DateTime endDate)
        {
            var invoices = await unitOfWork.InvoiceRepository.GetInvoicesByDateRange(startDate, endDate);

            if (invoices == null || !invoices.Any())
                throw new Exception("No invoices found between the specified dates.");

            var invoiceOutContracts = mapper.Map<List<InvoiceOutContract>>(invoices);

            return invoiceOutContracts;
        }
    
        public async Task<int> GetNextInvoiceNumber()
        {
            return await unitOfWork.InvoiceRepository.GetNextInvoiceNumber();
        }

        public async Task<InvoiceOutContract> UpdateInvoice(string id, InvoiceOperationsContract invoice)
        {
            var existingInvoice = await unitOfWork.InvoiceRepository.GetById(id) ?? throw new Exception("Invoice not found.");

            await unitOfWork.BeginTransactionAsync();

            try
            {
                foreach (var item in existingInvoice.Items)
                {
                    item.Product.Quantity += item.Quantity;
                }

                var updatedInvoice = mapper.Map(invoice, existingInvoice);

                foreach (var item in updatedInvoice.Items)
                {
                    var product = await unitOfWork.ProductRepository.GetById(item.ProductId) ?? throw new Exception("Product not found.");

                    product.Quantity -= item.Quantity;
                }

                await unitOfWork.InvoiceRepository.Update(updatedInvoice);

                await unitOfWork.Save();

                return mapper.Map<InvoiceOutContract>(updatedInvoice);
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync();
                throw new Exception("An error occurred while updating the invoice", ex);
            }
        }
    }
}
