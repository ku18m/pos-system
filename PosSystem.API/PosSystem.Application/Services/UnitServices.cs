using AutoMapper;
using PosSystem.Application.Contracts;
using PosSystem.Application.Contracts.Unit;
using PosSystem.Application.Interfaces.IServices;
using PosSystem.Core.Entities;

namespace PosSystem.Application.Services
{
    public class UnitServices(IUnitOfWork unitOfWork, IMapper mapper) : IUnitServices
    {
        public async Task<UnitOutContract> Add(UnitCreationContract unit)
        {
            if (unit == null)
                throw new Exception("Enter unit Name");

            Unit newUnit = mapper.Map<Unit>(unit);
            await unitOfWork.UnitRepository.Insert(newUnit);
            await unitOfWork.Save();

            return mapper.Map<UnitOutContract>(newUnit);
        }
        public async Task<List<UnitOutContract>> GetAll()
        {
            var units = await unitOfWork.UnitRepository.GetAll();
            return mapper.Map<List<UnitOutContract>>(units);
        }
        public async Task Delete(string id)
        {
            var unit = await unitOfWork.UnitRepository.GetById(id);
            if (unit == null)
                throw new Exception("Unit not found");

            await unitOfWork.UnitRepository.Delete(id);
            await unitOfWork.Save();
        }

        public async Task Edit(string id, UnitOperationsContract unit)
        {
            var existingUnit = await unitOfWork.UnitRepository.GetById(id);
            if (existingUnit == null)
                throw new Exception("Unit not found");

            mapper.Map(unit, existingUnit);
            await unitOfWork.UnitRepository.Update(existingUnit);
            await unitOfWork.Save();
        }
        public async Task<UnitOutContract> GetById(string id)
        {
            var unit = await unitOfWork.UnitRepository.GetById(id);
            if (unit == null)
                throw new Exception("Unit not found");

            return mapper.Map<UnitOutContract>(unit);
        }

        public async Task<List<UnitOutContract>> GetUnitsByName(string name)
        {
            var units = await unitOfWork.UnitRepository.GetUnitsByName(name);

            return mapper.Map<List<UnitOutContract>>(units);
        }

        public async Task<PaginatedOutContract<UnitOutContract>> GetPage(int page, int pageSize)
        {
            var totalPages = await unitOfWork.UnitRepository.GetTotalPages(pageSize);

            if (page > totalPages)
                throw new Exception("Page not found");

            var units = await unitOfWork.UnitRepository.GetPage(page, pageSize);

            return new PaginatedOutContract<UnitOutContract>
            {
                Data = mapper.Map<IEnumerable<UnitOutContract>>(units),
                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = totalPages
            };

        }

        public async Task<IEnumerable<UnitShortOutContract>> GetAllShorted()
        {
            var units = await unitOfWork.UnitRepository.GetAll();

            return mapper.Map<IEnumerable<UnitShortOutContract>>(units);
        }
    }
}
