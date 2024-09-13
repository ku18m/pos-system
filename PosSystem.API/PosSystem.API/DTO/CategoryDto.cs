
namespace PosSystem.API.DTO
{
    public class CategoryDTO
    {
        public string Id { get; set; }

        public string TypeName { get; set; }

        public string? Notes { get; set; }
        public virtual List<CompanyDTO> Companies { get; set; }
    }
}
