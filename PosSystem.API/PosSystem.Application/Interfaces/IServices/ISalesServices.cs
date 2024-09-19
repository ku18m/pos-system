using PosSystem.Application.Contracts.Sales;

namespace PosSystem.Application.Interfaces.IServices
{
    public interface ISalesServices
    {
        Task<SalesPeriodReportOutContract> GetPeriodReport(DateTime startDate, DateTime endDate);
    }
}
