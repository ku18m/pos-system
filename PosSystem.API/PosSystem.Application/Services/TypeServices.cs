using AutoMapper;
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

            var types = await unitOfWork.CategoryRepository.GetAll();
            var typeDB = types.FirstOrDefault(c => c.Name == type.Name);

            if (typeDB != null)
                throw new Exception("Type name already exists");

            var company = await unitOfWork.CompanyRepository.GetById(type.CompanyId);
            if (company == null)
                throw new Exception("Select a company");

            Unit newType = mapper.Map<Unit>(type);
            await unitOfWork.UnitRepository.Insert(newType);
            await unitOfWork.Save();

            return mapper.Map<TypeOutContract>(newType);
        }
        public async Task<List<TypeOutContract>> GetAll()
        {
            var types = await unitOfWork.CategoryRepository.GetAll();

            var typestoReturn = types.Select(type => new TypeOutContract
            {
                Id = type.CategoryId,
                Name = type.Name,
                Note = type.Notes,
                CompanyName = type.Company.Name
            }).ToList();

            return typestoReturn;
        }
        public async Task Delete(string id)
        {
            var unit = await unitOfWork.CategoryRepository.GetById(id);
            if (unit == null)
                throw new Exception("Type not found");

            await unitOfWork.UnitRepository.Delete(id);
            await unitOfWork.Save();
        }

        public async Task Edit(string id, AddTypeContract type)
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

        public async Task<TypeOutContract> GetByName(string name)
        {
            var unit = await unitOfWork.CategoryRepository.GetCategoryByName(name);
            if (unit == null)
                throw new Exception("Type not found");

            return mapper.Map<TypeOutContract>(unit);
        }
    }
}
