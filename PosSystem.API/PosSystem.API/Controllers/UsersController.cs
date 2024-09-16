using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PosSystem.Contracts.User;
using PosSystem.Core.Interfaces;

namespace PosSystem.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserServices<UserOperationsContract, UserOutContract> userServices) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserOperationsContract userFromRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await userServices.CreateUserAsync(userFromRequest);

            if (user == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserAsync()
        {
            var user = await userServices.GetUserAsync();

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
