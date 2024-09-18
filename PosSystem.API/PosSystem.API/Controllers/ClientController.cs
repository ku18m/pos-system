using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PosSystem.Application.Contracts.Client;
using PosSystem.Application.Interfaces.IServices;

namespace PosSystem.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController(IClientServices clientService) : ControllerBase
    {
        /// <summary>
        /// Add a new client.
        /// </summary>
        /// <param name="client">The client information.</param>
        /// <returns>The added client.</returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ClientCreationContract client)
        {
            if (client == null)
                return BadRequest("Client information must be provided.");

            if (ModelState.IsValid)
            {
                try
                {
                    var response = await clientService.Add(client);
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex.Message);
                }
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Get all clients.
        /// </summary>
        /// <returns>The list of clients.</returns>
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

        /// <summary>
        /// Get a page of clients.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>The page of clients.</returns>
        [HttpGet]
        public async Task<IActionResult> GetClientPage([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var clients = await clientService.GetClientPage(pageNumber, pageSize);
                return Ok(clients);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Get a client by ID.
        /// </summary>
        /// <param name="id">The client ID.</param>
        /// <returns>The client.</returns>
        [HttpGet("{id}")]
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

        /// <summary>
        /// Get a client by name.
        /// </summary>
        /// <param name="name">The client name.</param>
        /// <returns>The client.</returns>
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

        /// <summary>
        /// Get a client by phone number.
        /// </summary>
        /// <param name="phone">The client phone number.</param>
        /// <returns>The client.</returns>
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

        /// <summary>
        /// Get clients by address.
        /// </summary>
        /// <param name="address">The client address.</param>
        /// <returns>The list of clients.</returns>
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

        /// <summary>
        /// Get clients by name.
        /// </summary>
        /// <param name="name">The client name.</param>
        /// <returns>The list of clients.</returns>
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

        /// <summary>
        /// Delete a client by ID.
        /// </summary>
        /// <param name="id">The client ID.</param>
        /// <returns>No content.</returns>
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

        /// <summary>
        /// Edit a client by ID.
        /// </summary>
        /// <param name="id">The client ID.</param>
        /// <param name="client">The updated client information.</param>
        /// <returns>No content.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, [FromBody] ClientOperationsContract client)
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

        /// <summary>
        /// Get all clients in a short form.
        /// </summary>
        /// <returns>The list of clients.</returns>
        [HttpGet("GetAllShorted")]
        public async Task<IActionResult> GetAllShorted()
        {
            try
            {
                var clients = await clientService.GetAllShorted();
                return Ok(clients);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Get next client number from the database sequence.
        /// </summary>
        /// <returns>The next client number.</returns>
        [HttpGet("GetNextClientNumber")]
        public async Task<IActionResult> GetNextClientNumber()
        {
            try
            {
                var clientNumber = await clientService.GetNextClientNumber();
                return Ok(clientNumber);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
