using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PosSystem.Contracts.Company;
using PosSystem.Core.Entities;
using PosSystem.Core.Interfaces;
using PosSystem.Services;

namespace PosSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController(CompanyServices<AddCompanyContract, ReturnCompanyContract> service ) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddCompany(AddCompanyContract company)
        {
            if (company == null)
                return BadRequest("Enter Company Name");

            if (ModelState.IsValid)
            {
                try
                {
                    var createdCompany = await service.Add(company);
                    return Ok(createdCompany);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { Error = ex.Message });
                }
            }

            return BadRequest(ModelState);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var companies = await service.GetAll();
            if (companies == null || !companies.Any())
                return NotFound("No companies found");

            return Ok(companies);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var company = await service.GetById(id);
                return Ok(company);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await service.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] AddCompanyContract company)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await service.Edit(id, company);
                return Ok("Company updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("GetCompanyByName")]
        public async Task<IActionResult> GetCompanyByName( string name)
        {
            try
            {
                var company = await service.GetByName(name);
                return Ok(company);
            }
            catch (Exception ex)
            {
                return NotFound(new { Error = ex.Message });
            }
        }

        [HttpGet("GetCompaniesByName")]
        public async Task<IActionResult> GetCompaniesByName(string name)
        {
            try
            {
                var companies = await service.GetCompaniesByName(name);
                if (companies == null || !companies.Any())
                    return NotFound("No companies found with this name");

                return Ok(companies);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}