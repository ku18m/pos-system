using Microsoft.AspNetCore.Mvc;
using PosSystem.Application.Contracts.Unit;
using PosSystem.Application.Interfaces.IServices;

namespace PosSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController(IUnitServices<AddUnitContract, ReturnUnitContract> service) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddUnit(AddUnitContract unit)
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

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var units = await service.GetAll();
            if (units == null || !units.Any())
                return NotFound("No companies found");

            return Ok(units);
        }
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
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, [FromBody] AddUnitContract unit)
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
        [HttpGet("GetUnitByName")]
        public async Task<IActionResult> GetUnitByName(string name)
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
    }
}
