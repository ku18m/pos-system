using AutoMapper;
using PosSystem.Contracts.Company;
using PosSystem.Contracts.Unit;
using PosSystem.Core.Entities;
using PosSystem.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Services
{
    public class UnitServices<TUnitIn, TUnitOut>(IUnitOfWork unitOfWork, IMapper mapper) : IUnitServices<AddUnitContract, ReturnUnitContract>
         where TUnitIn : class
         where TUnitOut : class
    {
        public async Task<ReturnUnitContract> Add(AddUnitContract unit)
        {
            if (unit == null)
                throw new Exception("Enter unit Name");

            var units = await unitOfWork.UnitRepository.GetAll();
            var unitDB = units.FirstOrDefault(c => c.Name == unit.Name);

            if (unitDB != null)
                throw new Exception("unit name already exists");

            Unit newUnit = mapper.Map<Unit>(unit);
            await unitOfWork.UnitRepository.Insert(newUnit);
            await unitOfWork.Save();

            return mapper.Map<ReturnUnitContract>(newUnit);
        }
        public async Task<List<ReturnUnitContract>> GetAll()
        {
            var units = await unitOfWork.CompanyRepository.GetAll();
            return mapper.Map<List<ReturnUnitContract>>(units);
        }
        public async Task Delete(string id)
        {
            var unit = await unitOfWork.UnitRepository.GetById(id);
            if (unit == null)
                throw new Exception("Unit not found");

            await unitOfWork.UnitRepository.Delete(id);
            await unitOfWork.Save();
        }

        public async Task Edit(string id, AddUnitContract unit)
        {
            var existingUnit = await unitOfWork.UnitRepository.GetById(id);
            if (existingUnit == null)
                throw new Exception("Unit not found");

            mapper.Map(unit, existingUnit);
            await unitOfWork.UnitRepository.Update(existingUnit);
            await unitOfWork.Save();
        }
        public async Task<ReturnUnitContract> GetById(string id)
        {
            var unit = await unitOfWork.UnitRepository.GetById(id);
            if (unit == null)
                throw new Exception("Unit not found");

            return mapper.Map<ReturnUnitContract>(unit);
        }

        public async Task<ReturnUnitContract> GetByName(string name)
        {
            var unit = await unitOfWork.UnitRepository.GetUnitByName(name);
            if (unit == null)
                throw new Exception("Unit not found");

            return mapper.Map<ReturnUnitContract>(unit);
        }
    }
}
