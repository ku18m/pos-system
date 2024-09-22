using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PosSystem.Application.Contracts.Product;
using PosSystem.Application.Interfaces.IServices;

namespace PosSystem.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductServices productService) : ControllerBase
    {
        /// <summary>
        /// Add a new product.
        /// </summary>
        /// <param name="product">The product information.</param>
        /// <returns>The added product.</returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductCreationContract product)
        {
            if (product == null)
                return BadRequest("Product information must be provided.");

            if (ModelState.IsValid)
            {
                try
                {
                    var response = await productService.AddProduct(product);
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
        /// Get all products.
        /// </summary>
        /// <returns>The list of products.</returns>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var products = await productService.GetAllProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Get a page of products.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>The page of products.</returns>
        [HttpGet]
        public async Task<IActionResult> GetPage([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var products = await productService.GetProductsPage(pageNumber, pageSize);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Get a product by its ID.
        /// </summary>
        /// <param name="id">The product ID.</param>
        /// <returns>The product.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var product = await productService.GetProductById(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Update a product.
        /// </summary>
        /// <param name="id">The product id.</param>
        /// <param name="productUpdateContract">The product information.</param>
        /// <returns>The updated product.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] ProductOperationsContract productUpdateContract)
        {
            if (productUpdateContract == null)
                return BadRequest("Product information must be provided.");

            if (ModelState.IsValid)
            {
                try
                {
                    var response = await productService.UpdateProduct(id, productUpdateContract);
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
        /// Delete a product by its ID.
        /// </summary>
        /// <param name="id">The product ID.</param>
        /// <returns>The result of the deletion.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await productService.DeleteProduct(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Get products by category.
        /// </summary>
        /// <param name="categoryId">The category ID.</param>
        /// <returns>The list of products.</returns>
        [HttpGet("GetProductsByCategory")]
        public async Task<IActionResult> GetProductsByCategory(string categoryId)
        {
            try
            {
                var products = await productService.GetProductsByCategory(categoryId);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Get products by company.
        /// </summary>
        /// <param name="companyId">The company ID.</param>
        /// <returns>The list of products.</returns>
        [HttpGet("GetProductsByCompany")]
        public async Task<IActionResult> GetProductsByCompany(string companyId)
        {
            try
            {
                var products = await productService.GetProductsByCompany(companyId);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Get products by unit.
        /// </summary>
        /// <param name="unitId">The unit ID.</param>
        /// <returns>The list of products.</returns>
        [HttpGet("GetProductsByUnit")]
        public async Task<IActionResult> GetProductsByUnit(string unitId)
        {
            try
            {
                var products = await productService.GetProductsByUnit(unitId);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Get products by name.
        /// </summary>
        /// <param name="name">The product name.</param>
        /// <returns>The list of products.</returns>
        [HttpGet("GetProductsByName")]
        public async Task<IActionResult> GetProductsByName([FromQuery] string name)
        {
            try
            {
                var products = await productService.GetProductsByName(name);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Get all products in a short form.
        /// </summary>
        /// <returns>The list of all products in a short form.</returns>
        [HttpGet("GetAllShorted")]
        public async Task<IActionResult> GetAllShorted()
        {
            try
            {
                var products = await productService.GetAllProductsShorted();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
