using Microsoft.AspNetCore.Mvc;
using PosSystem.Application.Contracts.Client;
using PosSystem.Application.Interfaces.IServices;

namespace PosSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController(IClientServices<AddClientContract, ReturnClientContract> clientService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddClientContract client)
        {
            if (client == null)
                return BadRequest("Client information must be provided.");

            if (ModelState.IsValid)
            {
                try
                {
                    await clientService.Add(client);
                    return Ok(client);
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError("Error", ex.Message);
                }
            }
            return BadRequest(ModelState);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var clients = await clientService.GetAll();
                return Ok(clients);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var client = await clientService.GetById(id);
                return Ok(client);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                var client = await clientService.GetByName(name);
                return Ok(client);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("GetByPhone")]
        public async Task<IActionResult> GetByPhone(string phone)
        {
            try
            {
                var client = await clientService.GetByPhone(phone);
                return Ok(client);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("GetClientsByAddress")]
        public async Task<IActionResult> GetClientsByAddress(string address)
        {
            try
            {
                var clients = await clientService.GetClientsByAddress(address);
                return Ok(clients);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("GetClientsByName")]
        public async Task<IActionResult> GetClientsByName(string name)
        {
            try
            {
                var clients = await clientService.GetClientsByName(name);
                return Ok(clients);
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
                await clientService.DeleteById(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, [FromBody] AddClientContract client)
        {
            if (client == null)
                return BadRequest("Client information must be provided.");

            try
            {
                await clientService.Edit(id, client);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
