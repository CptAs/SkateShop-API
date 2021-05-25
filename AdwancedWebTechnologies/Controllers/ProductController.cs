using AdvancedWebTechnologies.Interfaces;
using AdvancedWebTechnologies.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace AdvancedWebTechnologies.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;

        }

        [Microsoft.AspNetCore.Mvc.HttpGet("{id}")]
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
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _service.GetProducts());
        }
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<IActionResult> CreateProduct([Microsoft.AspNetCore.Mvc.FromBody] ProductDto productModel, int categoryId, int producerId)
        {

            var product = await _service.CreateProduct(productModel.Name, productModel.Price, productModel.Description, productModel.Discount, categoryId, producerId);
            return CreatedAtAction(nameof(GetProductById), new { Id = product.Id }, product);
        }
        [Microsoft.AspNetCore.Mvc.HttpDelete("{id}")]
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
        [Microsoft.AspNetCore.Mvc.HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [Microsoft.AspNetCore.Mvc.FromBody] ProductDto product)
        {
            var p = await _service.UpdateProduct(id, product.Name, product.Price, product.Description, product.Discount);
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
        [Microsoft.AspNetCore.Mvc.HttpGet("category/{id}")]
        public async Task<IActionResult> GetProductsFromCategory(int id)
        {
            return Ok(await _service.GetProductsFromCategory(id));
        }
        [Microsoft.AspNetCore.Mvc.HttpGet("producer/{id}")]
        public async Task<IActionResult> GetProductsFromProducer(int id)
        {
            return Ok(await _service.GetProductsFromProducer(id));
        }
        [Microsoft.AspNetCore.Mvc.HttpGet("discount")]
        public async Task<IActionResult> GetProductsWithDiscount()
        {
           return Ok(await _service.GetProductsWithDiscount());
        }
        [Microsoft.AspNetCore.Mvc.HttpGet("{id}/othersFromCategory")]
        public async Task<IActionResult> GetRandomProductsFromCategory(int id)
        {
            return Ok(await _service.GetTenRandomProductsFromCategory(id));
        }
        [Microsoft.AspNetCore.Mvc.HttpGet("list")]
        public async Task<IActionResult> GetProductsFromListOfIds([FromUri] string idsInString)
        {
            if(idsInString == null)
            {
                return Ok(new List<int>());
            }
            var list = idsInString.Split(",");
            List<int> ids = new List<int>();
            foreach(string x in list)
            {
                ids.Add(int.Parse(x));
            }
            return Ok(await _service.GetProductsFromListOfIds(ids));
        }
        [Microsoft.AspNetCore.Mvc.HttpGet("bestsellers")]
        public async Task<IActionResult> GetBestsellingProducts()
        {
            return Ok(await _service.GetBestsellingProducts());
        }

    }
}
