using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PosSystem.Application.Contracts.Invoice;
using PosSystem.Application.Interfaces.IServices;
using System.Security.Claims;

namespace PosSystem.API.Controllers
{
    /// <summary>
    /// Controller for managing invoices.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController(IInvoiceServices invoiceService) : ControllerBase
    {
        /// <summary>
        /// Adds a new invoice.
        /// </summary>
        /// <param name="invoice">The invoice creation contract.</param>
        /// <returns>The added invoice.</returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] InvoiceCreationContract invoice)
        {
            if (invoice == null)
                return BadRequest("Invoice information must be provided.");

            if (invoice.EmployeeId == null)
                invoice.EmployeeId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (ModelState.IsValid)
            {
                try
                {
                    var response = await invoiceService.AddInvoice(invoice);
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Gets all invoices.
        /// </summary>
        /// <returns>All invoices.</returns>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var invoices = await invoiceService.GetAllInvoices();
                return Ok(invoices);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Gets a page of invoices.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>A page of invoices.</returns>
        [HttpGet]
        public async Task<IActionResult> GetPage([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var invoices = await invoiceService.GetInvoicesPage(pageNumber, pageSize);
                return Ok(invoices);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Gets an invoice by ID.
        /// </summary>
        /// <param name="id">The ID of the invoice.</param>
        /// <returns>The invoice with the specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var invoice = await invoiceService.GetInvoiceById(id);
                return Ok(invoice);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Updates an invoice.
        /// </summary>
        /// <param name="id">The ID of the invoice.</param>
        /// <param name="invoiceUpdateContract">The invoice update contract.</param>
        /// <returns>The updated invoice.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] InvoiceOperationsContract invoiceUpdateContract)
        {
            if (invoiceUpdateContract == null)
                return BadRequest("Invoice information must be provided.");

            if (ModelState.IsValid)
            {
                try
                {
                    var response = await invoiceService.UpdateInvoice(id, invoiceUpdateContract);
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
        /// Deletes an invoice.
        /// </summary>
        /// <param name="id">The ID of the invoice.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await invoiceService.DeleteInvoice(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Gets all invoices in a short form.
        /// </summary>
        /// <returns>All invoices in a shortform.</returns>
        [HttpGet("GetAllShorted")]
        public async Task<IActionResult> GetAllShorted()
        {
            try
            {
                var invoices = await invoiceService.GetAllInvoicesShorted();
                return Ok(invoices);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Gets the next invoice number.
        /// </summary>
        /// <returns>The next invoice number.</returns>
        [HttpGet("GetNextInvoiceNumber")]
        public async Task<IActionResult> GetNextInvoiceNumber()
        {
            try
            {
                var invoiceNumber = await invoiceService.GetNextInvoiceNumber();
                return Ok(new { invoiceNumber });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Gets invoices by date range.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>Invoices within the specified date range.</returns>
        [HttpGet("GetByDateRange")]
        public async Task<IActionResult> GetInvoicesByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            if (startDate == DateTime.MinValue || endDate == DateTime.MinValue)
                return BadRequest("Valid start date and end date must be provided.");

            try
            {
                var invoices = await invoiceService.GetInvoicesByDateRange(startDate, endDate);
                return Ok(invoices);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
