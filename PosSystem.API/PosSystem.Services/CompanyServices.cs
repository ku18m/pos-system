using AutoMapper;
using PosSystem.Contracts.Company;
using PosSystem.Core.Entities;
using PosSystem.Core.Interfaces;
using PosSystem.Core.Interfaces.Repositories;

namespace PosSystem.Services

{
    public class CompanyServices(IUnitOfWork unitOfWork, IMapper mapper) : ICompanyRepository
    {
        public async Task<ReturnCompanyContract> Add(AddCompanyContract company)
        {
            if (company == null)
                throw new Exception("Enter Company Name");

            var companies = await unitOfWork.CompanyRepository.GetAll();
            var companyDB = companies.FirstOrDefault(c => c.Name == company.Name);

            if (companyDB != null)
                throw new Exception("Company name already exists");

            Company newCompany = mapper.Map<Company>(company);

            await unitOfWork.CompanyRepository.Insert(newCompany);
            await unitOfWork.Save();

            return mapper.Map<ReturnCompanyContract>(newCompany);
        }

        public async Task Insert(Company company)
        {
            await unitOfWork.CompanyRepository.Insert(company);
            await unitOfWork.Save();
        }

        public async Task Update(Company entity)
        {
            var existingCompany = await unitOfWork.CompanyRepository.GetById(entity.CompanyId.ToString());
            if (existingCompany == null)
                throw new Exception("Company not found");

            mapper.Map(entity, existingCompany);

            await unitOfWork.CompanyRepository.Update(existingCompany);
            await unitOfWork.Save();
        }

        public async Task Delete(string id)
        {
            var company = await unitOfWork.CompanyRepository.GetById(id);
            if (company == null)
                throw new Exception("Company not found");

            await unitOfWork.CompanyRepository.Delete(id);
            await unitOfWork.Save();
        }

        public async Task<List<Company>> GetAll()
        {
            return await unitOfWork.CompanyRepository.GetAll();
        }

        public async Task<Company> GetById(string id)
        {
            var company = await unitOfWork.CompanyRepository.GetById(id);
            if (company == null)
                throw new Exception("Company not found");

            return company;
        }

        public async Task<IEnumerable<Company>> GetCompaniesByName(string name)
        {
            var companies = await unitOfWork.CompanyRepository.GetAll();
            return companies.Where(c => c.Name.Contains(name)).ToList();
        }

        public async Task<Company> GetCompanyByName(string name)
        {
            var companies = await unitOfWork.CompanyRepository.GetAll();

            var company = companies.FirstOrDefault(c => c.Name == name);

            if (company == null)
                throw new Exception("Company not found");

            return company;
        }
    }
}