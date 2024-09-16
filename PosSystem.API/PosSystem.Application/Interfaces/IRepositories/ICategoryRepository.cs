using PosSystem.Core.Entities;

namespace PosSystem.Application.Interfaces.IRepositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> GetCategoryByName(string name);
    }
}
