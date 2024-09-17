using PosSystem.Core.Entities;

namespace PosSystem.Application.Interfaces.IRepositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetProductByName(string name);
        Task<IEnumerable<Product>> GetProductsByName(string name);
        Task<IEnumerable<Product>> GetProductsByCategory(string categoryId);
        Task<IEnumerable<Product>> GetProductsByCompany(string companyId);
        Task<IEnumerable<Product>> GetProductsByUnit(string unitId);
    }
}
