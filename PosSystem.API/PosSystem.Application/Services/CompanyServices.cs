using AutoMapper;
using PosSystem.Application.Contracts;
using PosSystem.Application.Contracts.Company;
using PosSystem.Application.Interfaces.IServices;
using PosSystem.Core.Entities;

namespace PosSystem.Application.Services
{
    public class CompanyServices(IUnitOfWork unitOfWork, IMapper mapper) : ICompanyServices
    {
        public async Task<CompanyOutContract> Add(CompanyCreationContract company)
        {
            if (company == null)
                throw new Exception("Enter Company Name");

            Company newCompany = mapper.Map<Company>(company);

            await unitOfWork.CompanyRepository.Insert(newCompany);

            await unitOfWork.Save();

            return mapper.Map<CompanyOutContract>(newCompany);
        }

        public async Task<List<CompanyOutContract>> GetAll()
        {
            var companies = await unitOfWork.CompanyRepository.GetAll();

            return mapper.Map<List<CompanyOutContract>>(companies);
        }

        public async Task Delete(string id)
        {
            var company = await unitOfWork.CompanyRepository.GetById(id);

            if (company == null)
                throw new Exception("Company not found");

            await unitOfWork.CompanyRepository.Delete(id.ToString());
            await unitOfWork.Save();
        }

        public async Task Edit(string id, CompanyOperationsContract company)
        {
            var existingCompany = await unitOfWork.CompanyRepository.GetById(id);

            if (existingCompany == null)
                throw new Exception("Company not found");

            mapper.Map(company, existingCompany);

            await unitOfWork.CompanyRepository.Update(existingCompany);

            await unitOfWork.Save();
        }

        public async Task<CompanyOutContract> GetById(string id)
        {
            var company = await unitOfWork.CompanyRepository.GetById(id);

            if (company == null)
                throw new Exception("Company not found");

            return mapper.Map<CompanyOutContract>(company);
        }

        public async Task<CompanyOutContract> GetByName(string name)
        {
            var company = await unitOfWork.CompanyRepository.GetCompanyByName(name);

            if (company == null)
                throw new Exception("Company not found");

            return mapper.Map<CompanyOutContract>(company);
        }

        public async Task<IEnumerable<CompanyOutContract>> GetCompaniesByName(string name)
        {
            var companies = await unitOfWork.CompanyRepository.GetCompaniesByName(name);
            if (!companies.Any())
                throw new Exception("No companies found with this name");

            return mapper.Map<IEnumerable<CompanyOutContract>>(companies);
        }

        public async Task<PaginatedOutContract<CompanyOutContract>> GetCompanyPage(int pageNumber, int pageSize)
        {
            var totalPages = await unitOfWork.CompanyRepository.GetTotalPages(pageSize);

            if (pageNumber > totalPages)
                throw new Exception("Page number is greater than total pages.");

            var companies = await unitOfWork.CompanyRepository.GetPage(pageNumber, pageSize);


            return new PaginatedOutContract<CompanyOutContract>
            {
                Data = mapper.Map<List<CompanyOutContract>>(companies),
                TotalPages = totalPages,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<IEnumerable<CompanyShortOutContract>> GetAllShorted()
        {
            var companies = await unitOfWork.CompanyRepository.GetAll();

            return mapper.Map<IEnumerable<CompanyShortOutContract>>(companies);
        }
    }
}
