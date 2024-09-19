using PosSystem.Application.Contracts.Invoice;

namespace PosSystem.Application.Contracts.Sales
{
    public class SalesPeriodReportOutContract
    {
        public string TopClientByInvoices { get; set; }
        public int TopClientByInvoicesCount { get; set; }
        public string TopClientByPaidAmount { get; set; }
        public decimal TopClientPaidAmount { get; set; }
        public decimal TotalSellingsAmount { get; set; }
        public decimal TotalEarnedAmount { get; set; }
        public decimal TotalDueAmount { get; set; }
        public decimal TotalDiscounts { get; set; }
        public string TopEmployeeByInvoices { get; set; }
        public int TopEmployeeInvoicesCount { get; set; }
        public string TopEmployeeByTotalAmount { get; set; }
        public decimal TopEmployeeTotalAmount { get; set; }
        public IEnumerable<InvoiceShortOutContract> Invoices { get; set; } = new List<InvoiceShortOutContract>();
    }
}
