using Microsoft.AspNetCore.Mvc;
using PosSystem.Application.Contracts.User;
using PosSystem.Application.Interfaces.IServices;

namespace PosSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthServices authServices) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginContract userFromRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var token = await authServices.LoginUserAsync(userFromRequest.Email, userFromRequest.Password);

            if (token == null)
            {
                ModelState.AddModelError("Login", "Invalid email or password");
                return BadRequest(ModelState);
            }

            var response = new { token };

            return Ok(response);
        }
    }
}
