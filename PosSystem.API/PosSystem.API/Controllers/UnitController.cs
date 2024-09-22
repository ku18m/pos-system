using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PosSystem.Application.Contracts.Unit;
using PosSystem.Application.Interfaces.IServices;

namespace PosSystem.API.Controllers
{
    /// <summary>
    /// Controller for managing units.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController(IUnitServices service) : ControllerBase
    {
        /// <summary>
        /// Adds a new unit.
        /// </summary>
        /// <param name="unit">The unit to add.</param>
        /// <returns>The created unit.</returns>
        [HttpPost]
        public async Task<IActionResult> AddUnit(UnitCreationContract unit)
        {
            if (unit == null)
                return BadRequest("Enter Unit Name");

            if (ModelState.IsValid)
            {
                try
                {
                    var createdUnit = await service.Add(unit);
                    return Ok(createdUnit);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { Error = ex.Message });
                }
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Gets all units with pagination support.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>The paginated list of units.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, int pageSize = 10)
        {
            var units = await service.GetPage(pageNumber, pageSize);
            if (units == null || !units.Data.Any())
                return NotFound("No units found");

            return Ok(units);
        }

        /// <summary>
        /// Gets all units.
        /// </summary>
        /// <returns>The list of units.</returns>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var units = await service.GetAll();
            if (units == null || !units.Any())
                return NotFound("No companies found");

            return Ok(units);
        }

        /// <summary>
        /// Gets a unit by its ID.
        /// </summary>
        /// <param name="id">The ID of the unit.</param>
        /// <returns>The unit with the specified ID.</returns>
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
        /// Deletes a unit by its ID.
        /// </summary>
        /// <param name="id">The ID of the unit to delete.</param>
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
        /// Edits a unit by its ID.
        /// </summary>
        /// <param name="id">The ID of the unit to edit.</param>
        /// <param name="unit">The updated unit data.</param>
        /// <returns>A message indicating the success of the update.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, [FromBody] UnitOperationsContract unit)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await service.Edit(id, unit);
                return Ok("Unit updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        /// <summary>
        /// Gets units by name.
        /// </summary>
        /// <param name="name">The name of the units to retrieve.</param>
        /// <returns>The list of units with the specified name.</returns>
        [HttpGet("GetUnitsByName")]
        public async Task<IActionResult> GetUnitsByName(string name)
        {
            var company = await service.GetUnitsByName(name);
            return Ok(company);
        }

        /// <summary>
        /// Gets all units in a short form.
        /// </summary>
        /// <returns>The list of units in a short form.</returns>
        [HttpGet("GetAllShorted")]
        public async Task<IActionResult> GetAllShorted()
        {
            try
            {
                var units = await service.GetAllShorted();
                return Ok(units);
            }
            catch (Exception ex)
            {
                return NotFound(new { Error = ex.Message });
            }
        }
    }
}
