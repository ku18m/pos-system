using PosSystem.Core.Entities;

namespace PosSystem.Application.Interfaces.IRepositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category?> GetCategoryByName(string name);

        Task<IEnumerable<Category>> GetCategoriesByName(string name);

        Task<IEnumerable<Category>> GetCategoriesByCompanyId(string companyName);
    }
}
