using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PosSystem.Contracts.Company;
using PosSystem.Core.Entities;
using PosSystem.Services;

namespace PosSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController(CompanyServices service , IMapper mapper) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult>  AddCompany(AddCompanyContract company)
        {
            if (company == null)
                return BadRequest("Enter Company Name");

            if (ModelState.IsValid)
            {
                try
                {
                    await service.Add(company); 
                    return Ok(company);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex.Message);
                }
            }

            return BadRequest(ModelState);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var companies = await service.GetAll();
            if (companies == null || !companies.Any())
                return NotFound("No companies found");

            return Ok(companies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var company = await service.GetById(id);
            if (company == null)
                return NotFound();

            return Ok(company);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await service.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, [FromBody] AddCompanyContract companyDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var company = mapper.Map<Company>(companyDto);
                company.CompanyId = id; 

                await service.Update(company);
                return Ok("Company updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("search")]
        public async Task<IActionResult> GetCompanyByName([FromQuery] string name)
        {
            try
            {
                var company = await service.GetCompanyByName(name);
                var companyContract = mapper.Map<ReturnCompanyContract>(company);
                return Ok(companyContract);
            }
            catch (Exception)
            {
                return NotFound("Company not found with the given name");
            }
        }
        [HttpGet("search/{name}")]
        public async Task<IActionResult> GetCompaniesByName(string name)
        {
            var companies = await service.GetCompaniesByName(name);
            if (companies == null || !companies.Any())
                return NotFound("No companies found with the given name");

            var companyContracts = companies.Select(c => mapper.Map<ReturnCompanyContract>(c));
            return Ok(companyContracts);
        }
    }
   
}
