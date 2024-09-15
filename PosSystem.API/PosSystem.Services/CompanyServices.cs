using AutoMapper;
using PosSystem.Core.Entities;
using PosSystem.Core.Interfaces;
using PosSystem.Contracts.Company;

namespace PosSystem.Services
{
    public class CompanyServices<TCompanyIn, TCompanyOut>(IUnitOfWork unitOfWork, IMapper mapper) : ICompanyServices<AddCompanyContract, ReturnCompanyContract>
         where TCompanyIn : class
         where TCompanyOut : class
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

        public async Task<List<ReturnCompanyContract>> GetAll()
        {
            var companies = await unitOfWork.CompanyRepository.GetAll();
            return mapper.Map<List<ReturnCompanyContract>>(companies);
        }

        public async Task Delete(string id)
        {
            var company = await unitOfWork.CompanyRepository.GetById(id);
            if (company == null)
                throw new Exception("Company not found");

            await unitOfWork.CompanyRepository.Delete(id.ToString());
            await unitOfWork.Save();
        }

        public async Task Edit(string id, AddCompanyContract company)
        {
            var existingCompany = await unitOfWork.CompanyRepository.GetById(id);
            if (existingCompany == null)
                throw new Exception("Company not found");

            mapper.Map(company, existingCompany);
            await unitOfWork.CompanyRepository.Update(existingCompany);
            await unitOfWork.Save();
        }

        public async Task<ReturnCompanyContract> GetById(string id)
        {
            var company = await unitOfWork.CompanyRepository.GetById(id);
            if (company == null)
                throw new Exception("Company not found");

            return mapper.Map<ReturnCompanyContract>(company);
        }

        public async Task<ReturnCompanyContract> GetByName(string name)
        {
            var company = await unitOfWork.CompanyRepository.GetCompanyByName(name);
            if (company == null)
                throw new Exception("Company not found");

            return mapper.Map<ReturnCompanyContract>(company);
        }

        public async Task<IEnumerable<ReturnCompanyContract>> GetCompaniesByName(string name)
        {
            var companies = await unitOfWork.CompanyRepository.GetCompaniesByName(name);
            if (!companies.Any())
                throw new Exception("No companies found with this name");

            return mapper.Map<IEnumerable<ReturnCompanyContract>>(companies);
        }
    }
}
