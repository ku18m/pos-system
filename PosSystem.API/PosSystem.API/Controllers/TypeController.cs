using Microsoft.AspNetCore.Mvc;
using PosSystem.Application.Contracts.Type;
using PosSystem.Application.Interfaces.IServices;

namespace PosSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController(ITypeServices<AddTypeContract, ReturnTypeContract> service) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddType(AddTypeContract type)
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

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var types = await service.GetAll();
            if (types == null || !types.Any())
                return NotFound("No Types found");

            return Ok(types);
        }
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
        public async Task<IActionResult> Edit(string id, [FromBody] AddTypeContract type)
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
        [HttpGet("GetTypeByName")]
        public async Task<IActionResult> GetTypeByName(string name)
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
