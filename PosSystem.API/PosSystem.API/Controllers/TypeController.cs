using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PosSystem.Application.Contracts.Type;
using PosSystem.Application.Interfaces.IServices;

namespace PosSystem.API.Controllers
{
    /// <summary>
    /// Controller for managing types.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController(ITypeServices service) : ControllerBase
    {
        /// <summary>
        /// Adds a new type.
        /// </summary>
        /// <param name="type">The type to add.</param>
        /// <returns>The created type.</returns>
        [HttpPost]
        public async Task<IActionResult> AddType(TypeCreationContract type)
        {
            if (type == null)
                return BadRequest("Enter Type Name");

            if (ModelState.IsValid)
            {
                try
                {
                    var createdType = await service.Add(type);
                    return Ok(createdType);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { Error = ex.Message });
                }
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Gets all types with pagination support.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size. Default is 10.</param>
        /// <returns>The paginated list of types.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var types = await service.GetTypePage(pageNumber, pageSize);
                return Ok(types);
            }
            catch (Exception ex)
            {
                return NotFound(new { Error = ex.Message });
            }
        }

        /// <summary>
        /// Gets all types.
        /// </summary>
        /// <returns>The list of types.</returns>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var types = await service.GetAll();
            if (types == null || !types.Any())
                return NotFound("No Types found");

            return Ok(types);
        }

        /// <summary>
        /// Gets a type by its ID.
        /// </summary>
        /// <param name="id">The ID of the type.</param>
        /// <returns>The type with the specified ID.</returns>
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var type = await service.GetById(id);
                return Ok(type);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a type by its ID.
        /// </summary>
        /// <param name="id">The ID of the type to delete.</param>
        /// <returns>No content if the type is deleted successfully.</returns>
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
        /// Edits a type by its ID.
        /// </summary>
        /// <param name="id">The ID of the type to edit.</param>
        /// <param name="type">The updated type.</param>
        /// <returns>A message indicating the success of the update.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, [FromBody] TypeOperationsContract type)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await service.Edit(id, type);
                return Ok("Type updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        /// <summary>
        /// Gets a type by its name.
        /// </summary>
        /// <param name="name">The name of the type.</param>
        /// <returns>The type with the specified name.</returns>
        [HttpGet("GetTypeByName")]
        public async Task<IActionResult> GetTypeByName(string name)
        {
            try
            {
                var company = await service.GetTypesByName(name);
                return Ok(company);
            }
            catch (Exception ex)
            {
                return NotFound(new { Error = ex.Message });
            }
        }

        /// <summary>
        /// Gets all types associated with a company.
        /// </summary>
        /// <param name="companyId">The ID of the company.</param>
        /// <returns>The list of types associated with the company.</returns>
        [HttpGet("GetByCompanyId")]
        public async Task<IActionResult> GetByCompanyId(string companyId)
        {
            try
            {
                var types = await service.GetByCompanyId(companyId);
                return Ok(types);
            }
            catch (Exception ex)
            {
                return NotFound(new { Error = ex.Message });
            }
        }

        /// <summary>
        /// Gets all types sorted.
        /// </summary>
        /// <returns>The list of types sorted.</returns>
        [HttpGet("GetTypeShorted")]
        public async Task<IActionResult> GetTypeShorted()
        {
            try
            {
                var types = await service.GetAllShorted();
                return Ok(types);
            }
            catch (Exception ex)
            {
                return NotFound(new { Error = ex.Message });
            }
        }
    }
}
