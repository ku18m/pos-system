using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PosSystem.Application.Contracts.User;
using PosSystem.Application.Interfaces.IServices;
using System.Security.Claims;

namespace PosSystem.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserServices _userServices) : ControllerBase
    {
        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="userFromRequest">The user creation contract.</param>
        /// <returns>The created user.</returns>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserOutContract), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserCreationContract userFromRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userServices.CreateUserAsync(userFromRequest);

            if (user == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(user);
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>The list of all users.</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<UserOutContract>), 200)]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _userServices.GetAllUsersAsync();

            return Ok(users);
        }

        /// <summary>
        /// Gets the current user.
        /// </summary>
        /// <returns>The current user.</returns>
        [HttpGet("current")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserOutContract), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetUserAsync()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _userServices.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        /// <summary>
        /// Gets a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>The user with the specified ID.</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserOutContract), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetUserByIdAsync(string id)
        {
            var user = await _userServices.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        /// <summary>
        /// Updates a user.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <param name="userToUpdate">The user operations contract.</param>
        /// <returns>The updated user.</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserOutContract), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateUserAsync(string id, [FromBody] UserOperationsContract userToUpdate)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userServices.UpdateUserAsync(id, userToUpdate);

            if (user == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(user);
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>The result of the deletion.</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteUserByIdAsync(string id)
        {
            var result = await _userServices.DeleteUserByIdAsync(id);

            if (result == 404)
            {
                return NotFound();
            }

            if (result == 500)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(result);
        }

        /// <summary>
        /// Updates a user's password.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>The result of the password update.</returns>
        [HttpPut("password/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateUserPassword(string id, [FromBody] string newPassword)
        {
            var result = await _userServices.UpdateUserPassword(id, newPassword);

            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok("Password updated successfully");
        }
    }
}
