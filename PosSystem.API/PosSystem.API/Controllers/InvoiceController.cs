using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PosSystem.Application.Contracts.Invoice;
using PosSystem.Application.Contracts.Product;
using PosSystem.Application.Interfaces.IServices;
using PosSystem.Application.Services;

namespace PosSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController(IInvoiceServices invoiceService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] InvoiceCreationContract invoice)
        {
            if (invoice == null)
                return BadRequest("Invoice information must be provided.");

            if (ModelState.IsValid)
            {
                try
                {
                    var response = await invoiceService.AddInvoice(invoice);
                    return Ok(response);
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
                var invoices = await invoiceService.GetAllInvoices();
                return Ok(invoices);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetPage([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var invoices = await invoiceService.GetInvoicePage(pageNumber, pageSize);
                return Ok(invoices);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
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
        [HttpGet("GetNextInvoiceNumber")]
        public async Task<IActionResult> GetNextClientNumber()
        {
            try
            {
                var invoiceNumber = await invoiceService.GetNextInvoiceNumber();
                return Ok(invoiceNumber);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
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
