using PosSystem.Application.Contracts;
using PosSystem.Application.Contracts.Product;

namespace PosSystem.Application.Interfaces.IServices
{
    public interface IProductServices
    {
        Task<ProductOutContract> AddProduct(ProductCreationContract product);
        Task<IEnumerable<ProductOutContract>> GetAllProducts();
        Task DeleteProduct(string id);
        Task<ProductOutContract> UpdateProduct(string id, ProductOperationsContract product);
        Task<ProductOutContract> GetProductById(string id);
        Task<IEnumerable<ProductOutContract>> GetProductsByName(string name);
        Task<IEnumerable<ProductOutContract>> GetProductsByCategory(string categoryId);
        Task<IEnumerable<ProductOutContract>> GetProductsByCompany(string companyId);
        Task<IEnumerable<ProductOutContract>> GetProductsByUnit(string unitId);
        Task<PaginatedOutContract<ProductOutContract>> GetProductsPage(int pageNumber, int pageSize);
        Task<IEnumerable<ProductShortOutContract>> GetAllProductsShorted();
    }
}
