using AutoMapper;
using PosSystem.Application.Contracts;
using PosSystem.Application.Contracts.Invoice;
using PosSystem.Application.Contracts.Product;
using PosSystem.Application.Interfaces.IServices;
using PosSystem.Core.Entities;


namespace PosSystem.Application.Services
{
    public class InvoiceService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        public async Task<InvoiceOutContract> AddInvoice(InvoiceCreationContract invoice)
        {
            var client = unitOfWork.ClientRepository.GetById(invoice.ClientId);
            if (client == null)
                throw new Exception($"The client with id {invoice.ClientId} does not exist");
            var employee = unitOfWork.UserRepository.GetById(invoice.EmployeeId);
       
            foreach (var item in invoice.InvoiceItems)
            {
                var itemDb = unitOfWork.ProductRepository.GetById(item.ItemId);
                if (itemDb == null)
                    throw new Exception($"Item with Id {item.ItemId} does not exist ");
            }
            var newInvoice = mapper.Map<Invoice>(invoice);

            await unitOfWork.InvoiceRepository.Insert(newInvoice);
            try
            {
                await unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured while creating the invoice");
            }

            return mapper.Map<InvoiceOutContract>(newInvoice);
        }
        public async Task<IEnumerable<InvoiceOutContract>> GetAllInvoices()
        {
            var Invoices = await unitOfWork.InvoiceRepository.GetAll();

            return mapper.Map<IEnumerable<InvoiceOutContract>>(Invoices);
        }
        public async Task<IEnumerable<InvoiceShortOutContract>> GetAllInvoicesShorted()
        {
            var Invoices = await unitOfWork.ProductRepository.GetAll();

            return mapper.Map<IEnumerable<InvoiceShortOutContract>>(Invoices);
        }
        public async Task DeleteInvoice(string id)
        {
            var invoice = await unitOfWork.InvoiceRepository.GetById(id);

            if (invoice == null)
                throw new Exception("invoice not found.");

            await unitOfWork.InvoiceRepository.Delete(id);

            await unitOfWork.Save();
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
        public async Task<List<InvoiceOutContract>> GetInvoicesByDateRange(DateTime startDate, DateTime endDate)
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
    }
}
