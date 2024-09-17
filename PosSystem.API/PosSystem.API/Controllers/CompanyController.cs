using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PosSystem.Application.Contracts.Company;
using PosSystem.Application.Interfaces.IServices;

namespace PosSystem.API.Controllers
{
    /// <summary>
    /// Represents the controller for managing companies.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController(ICompanyServices service) : ControllerBase
    {
        /// <summary>
        /// Adds a new company.
        /// </summary>
        /// <param name="company">The company creation contract.</param>
        /// <returns>The created company.</returns>
        [HttpPost]
        public async Task<IActionResult> AddCompany([FromBody] CompanyCreationContract company)
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

        /// <summary>
        /// Gets all companies.
        /// </summary>
        /// <returns>The list of all companies.</returns>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var companies = await service.GetAll();
            if (companies == null || !companies.Any())
                return NotFound("No companies found");

            return Ok(companies);
        }

        /// <summary>
        /// Gets a page of companies.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size (default is 10).</param>
        /// <returns>The page of companies.</returns>
        [HttpGet]
        public async Task<IActionResult> GetCompanyPage([FromQuery] int pageNumber, [FromQuery] int pageSize = 10)
        {
            try
            {
                var companies = await service.GetCompanyPage(pageNumber, pageSize);
                return Ok(companies);
            }
            catch (Exception ex)
            {
                return NotFound(new { Error = ex.Message });
            }
        }

        /// <summary>
        /// Gets a company by its ID.
        /// </summary>
        /// <param name="id">The company ID.</param>
        /// <returns>The company with the specified ID.</returns>
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string id)
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

        /// <summary>
        /// Deletes a company by its ID.
        /// </summary>
        /// <param name="id">The company ID.</param>
        /// <returns>No content.</returns>
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
                return BadRequest(new { Error = ex.Message });
            }
        }

        /// <summary>
        /// Edits a company by its ID.
        /// </summary>
        /// <param name="id">The company ID.</param>
        /// <param name="company">The company operations contract.</param>
        /// <returns>A message indicating the success of the operation.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, [FromBody] CompanyOperationsContract company)
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

        /// <summary>
        /// Gets a company by its name.
        /// </summary>
        /// <param name="name">The company name.</param>
        /// <returns>The company with the specified name.</returns>
        [HttpGet("GetCompanyByName")]
        public async Task<IActionResult> GetCompanyByName(string name)
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

        /// <summary>
        /// Gets companies by name.
        /// </summary>
        /// <param name="name">The company name.</param>
        /// <returns>The list of companies with the specified name.</returns>
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

        /// <summary>
        /// Gets all companies sorted.
        /// </summary>
        /// <returns>The list of all companies sorted.</returns>
        [HttpGet("GetAllShorted")]
        public async Task<IActionResult> GetAllShorted()
        {
            try
            {
                var companies = await service.GetAllShorted();
                return Ok(companies);
            }
            catch (Exception ex)
            {
                return NotFound(new { Error = ex.Message });
            }
        }
    }
}