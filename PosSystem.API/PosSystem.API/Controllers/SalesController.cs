using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PosSystem.Application.Interfaces.IServices;

namespace PosSystem.API.Controllers
{
    /// <summary>
    /// Controller for getting sales reports.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController(ISalesServices salesServices) : ControllerBase
    {
        /// <summary>
        /// Get the period report for sales.
        /// </summary>
        /// <param name="startDate">The start date of the period.</param>
        /// <param name="endDate">The end date of the period.</param>
        /// <returns>The period report for sales.</returns>
        [HttpGet("GetPeriodReport")]
        public async Task<IActionResult> GetPeriodReport([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var response = await salesServices.GetPeriodReport(startDate, endDate);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
