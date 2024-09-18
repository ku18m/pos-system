using AutoMapper;
using PosSystem.Application.Contracts;
using PosSystem.Application.Contracts.Type;
using PosSystem.Application.Interfaces.IServices;
using PosSystem.Core.Entities;


namespace PosSystem.Application.Services
{
    public class TypeServices(IUnitOfWork unitOfWork, IMapper mapper) : ITypeServices
    {
        public async Task<TypeOutContract> Add(TypeCreationContract type)
        {
            if (type == null)
                throw new Exception("Enter type Name");

            Category newType = mapper.Map<Category>(type);
            await unitOfWork.CategoryRepository.Insert(newType);
            await unitOfWork.Save();

            return mapper.Map<TypeOutContract>(newType);
        }

        public async Task<List<TypeOutContract>> GetAll()
        {
            var types = await unitOfWork.CategoryRepository.GetAll();

            
            return mapper.Map<List<TypeOutContract>>(types);
        }

        public async Task Delete(string id)
        {
            var unit = await unitOfWork.CategoryRepository.GetById(id);
            if (unit == null)
                throw new Exception("Type not found");

            await unitOfWork.UnitRepository.Delete(id);
            await unitOfWork.Save();
        }

        public async Task Edit(string id, TypeOperationsContract type)
        {
            var existingType = await unitOfWork.CategoryRepository.GetById(id);
            if (existingType == null)
                throw new Exception("Type not found");

            mapper.Map(type, existingType);
            await unitOfWork.CategoryRepository.Update(existingType);
            await unitOfWork.Save();
        }
        public async Task<TypeOutContract> GetById(string id)
        {
            var unit = await unitOfWork.CategoryRepository.GetById(id);
            if (unit == null)
                throw new Exception("Type not found");

            return mapper.Map<TypeOutContract>(unit);
        }

        public async Task<IEnumerable<TypeOutContract>> GetTypesByName(string name)
        {
            var unit = await unitOfWork.CategoryRepository.GetCategoryByName(name);
            if (unit == null)
                throw new Exception("Type not found");

            return mapper.Map<IEnumerable<TypeOutContract>>(unit);
        }

        public async Task<IEnumerable<TypeOutContract>> GetByCompanyId(string companyId)
        {
            var types = await unitOfWork.CategoryRepository.GetCategoriesByCompanyId(companyId);

            return mapper.Map<IEnumerable<TypeOutContract>>(types);
        }

        public async Task<PaginatedOutContract<TypeOutContract>> GetTypePage(int page, int pageSize)
        {
            var totalPages = await unitOfWork.CategoryRepository.GetTotalPages(pageSize);

            if (page > totalPages)
                throw new Exception("Page not found");

            var types = await unitOfWork.CategoryRepository.GetPage(page, pageSize);

            return new PaginatedOutContract<TypeOutContract>
            {
                Data = mapper.Map<IEnumerable<TypeOutContract>>(types),
                TotalPages = totalPages,
                PageSize = pageSize,
                CurrentPage = page
            };
        }

        public async Task<IEnumerable<TypeShortOutContract>> GetAllShorted()
        {
            var types = await unitOfWork.CategoryRepository.GetAll();

            return mapper.Map<IEnumerable<TypeShortOutContract>>(types);
        }
    }
}
