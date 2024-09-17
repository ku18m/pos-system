namespace PosSystem.Application.Contracts
{
    public class PaginatedOutContract<Type>
    {
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<Type> Data { get; set; }
    }
}
