using AutoMapper;
using PosSystem.Application.Contracts.Invoice;
using PosSystem.Application.Contracts.Sales;
using PosSystem.Application.Interfaces.IRepositories;
using PosSystem.Application.Interfaces.IServices;

namespace PosSystem.Application.Services
{
    public class SalesServices(IUnitOfWork unitOfWork, IMapper mapper) : ISalesServices
    {
        public async Task<SalesPeriodReportOutContract> GetPeriodReport(DateTime startDate, DateTime endDate)
        {
            var invoices = await unitOfWork.InvoiceRepository.GetInvoicesByDateRange(startDate, endDate);

            // Top client based on number of invoices
            var topClientByInvoices = invoices.GroupBy(i => i.Client)
                .OrderByDescending(g => g.Count())
                .Select(g => new
                {
                    Client = g.Key.Name,
                    InvoicesCount = g.Count()
                }).FirstOrDefault();

            // Top client based on most paid amount
            var topClientByPaidAmount = invoices.GroupBy(i => i.Client)
                .OrderByDescending(g => g.Sum(i => i.PaidAmount))
                .Select(g => new
                {
                    Client = g.Key.Name,
                    TotalPaidAmount = g.Sum(i => i.PaidAmount)
                })
                .FirstOrDefault();

            // Total sellings amount
            var totalSellingsAmount = invoices.Sum(i => i.TotalAmount);

            // Total earned amount
            var totalEarnedAmount = invoices.Sum(i => i.FinalAmount);

            // Total due amount
            var totalDueAmount = invoices.Sum(i => i.DueAmount);

            // Total discounts
            var totalDiscounts = invoices.Sum(i => i.TotalDiscount);

            // Top employee based on number of invoices
            var topEmployeeByInvoices = invoices.GroupBy(i => i.User)
                .OrderByDescending(g => g.Count())
                .Select(g => new
                {
                    Employee = g.Key.FullName,
                    InvoicesCount = g.Count()
                })
                .FirstOrDefault();

            // Top employee based on the total selling amount
            var topEmployeeByTotalAmount = invoices.GroupBy(i => i.User)
                .OrderByDescending(g => g.Sum(i => i.TotalAmount))
                .Select(g => new
                {
                    Employee = g.Key.FullName,
                    TotalAmount = g.Sum(i => i.TotalAmount)
                })
                .FirstOrDefault();

            // Prepare the DTO
            var salesReport = new SalesPeriodReportOutContract
            {
                TopClientByInvoices = topClientByInvoices?.Client,
                TopClientByInvoicesCount = topClientByInvoices?.InvoicesCount ?? 0,
                TopClientByPaidAmount = topClientByPaidAmount?.Client,
                TopClientPaidAmount = topClientByPaidAmount?.TotalPaidAmount ?? 0,
                TotalSellingsAmount = totalSellingsAmount,
                TotalEarnedAmount = totalEarnedAmount,
                TotalDueAmount = totalDueAmount,
                TotalDiscounts = totalDiscounts,
                TopEmployeeByInvoices = topEmployeeByInvoices?.Employee,
                TopEmployeeInvoicesCount = topEmployeeByInvoices?.InvoicesCount ?? 0,
                TopEmployeeByTotalAmount = topEmployeeByTotalAmount?.Employee,
                TopEmployeeTotalAmount = topEmployeeByTotalAmount?.TotalAmount ?? 0
            };

            salesReport.Invoices = mapper.Map<IEnumerable<InvoiceShortOutContract>>(invoices);

            return salesReport;
        }
    }
}
