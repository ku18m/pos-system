using AutoMapper;
using PosSystem.Application.Contracts;
using PosSystem.Application.Contracts.Product;
using PosSystem.Application.Interfaces.IServices;
using PosSystem.Core.Entities;

namespace PosSystem.Application.Services
{
    public class ProductServices(IUnitOfWork unitOfWork, IMapper mapper) : IProductServices
    {
        public async Task<ProductOutContract> AddProduct(ProductCreationContract product)
        {
            var newProduct = mapper.Map<Product>(product);

            await unitOfWork.ProductRepository.Insert(newProduct);

            try
            {
                await unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured while creating the product");
            }

            return mapper.Map<ProductOutContract>(newProduct);
        }

        public async Task DeleteProduct(string id)
        {
            var product = await unitOfWork.ProductRepository.GetById(id);

            if (product == null)
                throw new Exception("Product not found.");

            await unitOfWork.ProductRepository.Delete(id);

            await unitOfWork.Save();
        }

        public async Task<IEnumerable<ProductOutContract>> GetAllProducts()
        {
            var products = await unitOfWork.ProductRepository.GetAll();

            return mapper.Map<IEnumerable<ProductOutContract>>(products);
        }

        public async Task<IEnumerable<ProductShortOutContract>> GetAllProductsShorted()
        {
            var products = await unitOfWork.ProductRepository.GetAll();

            return mapper.Map<IEnumerable<ProductShortOutContract>>(products);
        }

        public async Task<ProductOutContract> GetProductById(string id)
        {
            var product = await unitOfWork.ProductRepository.GetById(id);

            if (product == null)
                throw new Exception("Product not found.");

            return mapper.Map<ProductOutContract>(product);
        }

        public async Task<IEnumerable<ProductOutContract>> GetProductsByCategory(string categoryId)
        {
            var products = await unitOfWork.ProductRepository.GetProductsByCategory(categoryId);

            return mapper.Map<IEnumerable<ProductOutContract>>(products);
        }

        public async Task<IEnumerable<ProductOutContract>> GetProductsByCompany(string companyId)
        {
            var products = await unitOfWork.ProductRepository.GetProductsByCompany(companyId);

            return mapper.Map<IEnumerable<ProductOutContract>>(products);
        }

        public async Task<IEnumerable<ProductOutContract>> GetProductsByName(string name)
        {
            var products = await unitOfWork.ProductRepository.GetProductsByName(name);

            return mapper.Map<IEnumerable<ProductOutContract>>(products);
        }

        public async Task<IEnumerable<ProductOutContract>> GetProductsByUnit(string unitId)
        {
            var products = await unitOfWork.ProductRepository.GetProductsByUnit(unitId);

            return mapper.Map<IEnumerable<ProductOutContract>>(products);
        }

        public async Task<PaginatedOutContract<ProductOutContract>> GetProductsPage(int pageNumber, int pageSize)
        {
            var totalPages = await unitOfWork.ProductRepository.GetTotalPages(pageSize);

            if (pageNumber > totalPages)
                throw new Exception("Page number is greater than total pages.");

            var products = await unitOfWork.ProductRepository.GetPage(pageNumber, pageSize);

            return new PaginatedOutContract<ProductOutContract>
            {
                Data = mapper.Map<IEnumerable<ProductOutContract>>(products),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<ProductOutContract> UpdateProduct(string id, ProductOperationsContract product)
        {
            var productToUpdate = await unitOfWork.ProductRepository.GetById(id);

            if (productToUpdate == null)
                throw new Exception("Product not found.");

            mapper.Map(product, productToUpdate);

            await unitOfWork.ProductRepository.Update(productToUpdate);

            try
            {
                await unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured while updating the product");
            }

            return mapper.Map<ProductOutContract>(productToUpdate);
        }
    }
}
