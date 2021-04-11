using AdvancedWebTechnologies.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvancedWebTechnologies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IProducerService _proService;
        private readonly ICategoryService _catService;
        public ProductController(IProductService service, IProducerService proService, ICategoryService catService)
        {
            _service = service;
            _catService = catService;
            _proService = proService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var p = await _service.GetProductByIdAsync(id);
            if (p == null)
            {
                var problem = new ProblemDetails
                {
                    Instance = HttpContext.Request.Path,
                    Status = StatusCodes.Status404NotFound,
                    Type = $"https://httpstatuses.com/404",
                    Title = "Not found",
                    Detail = $"Product {id} does not exist or you do not have access to it."
                };

                return NotFound(problem);
            }
            return Ok(p);
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _service.GetProducts());
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(string name, decimal price, string description, int discount, int categoryId, int producerId)
        {
            var category = await _catService.GetCategoryByIdAsync(categoryId);
            var producer = await _proService.GetByIdAsync(producerId);
            var product = await _service.CreateProduct(name, price, description, discount, category, producer);
            return CreatedAtAction(nameof(GetProductById), new { Id = product.Id }, product);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var p = await _service.DeleteProduct(id);
            if (p == null)
            {
                var problem = new ProblemDetails
                {
                    Instance = HttpContext.Request.Path,
                    Status = StatusCodes.Status404NotFound,
                    Type = $"https://httpstatuses.com/404",
                    Title = "Not found",
                    Detail = $"Product {id} does not exist or you do not have access to it."
                };

                return NotFound(problem);
            }
            return Ok(p);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, string name, decimal price, string description, int discount)
        {
            var p = await _service.UpdateProduct(id, name, price, description, discount);
            if (p == null)
            {
                var problem = new ProblemDetails
                {
                    Instance = HttpContext.Request.Path,
                    Status = StatusCodes.Status404NotFound,
                    Type = $"https://httpstatuses.com/404",
                    Title = "Not found",
                    Detail = $"Category {id} does not exist or you do not have access to it."
                };

                return NotFound(problem);
            }
            return Ok(p);
        }
    }
}
