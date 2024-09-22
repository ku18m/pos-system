namespace PosSystem.Application.Contracts.Invoice
{
    public class InvoiceShortOutContract
    {
        public string Id { get; set; }
        public int Number { get; set; }

        public DateTime BillDate { get; set; }
        public DateTime Date { get; set; }
        public decimal PaidUp { get; set; }
        public decimal Net { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal TotalAmount { get; set; }
        public string ClientName { get; set; }
        public string EmployeeName { get; set; }
    }
}
