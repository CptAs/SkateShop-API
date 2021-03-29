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
    public class CategoryController:ControllerBase
    {
        private readonly ICategoryService _service;
        public CategoryController(ICategoryService service)
        {
            _service = service;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var cat = await _service.GetCategoryByIdAsync(id);
            if (cat == null)
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
            return Ok(cat);
        }
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            return Ok(await _service.GetCategories());
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(string name, int parrentId)
        {
            var parrent = await _service.GetCategoryByIdAsync(parrentId);
            var category = await _service.CreateCategory(name, parrent);
            return CreatedAtAction(nameof(GetCategoryById), new { Id = category.CategoryId }, category);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var cat = await _service.DeleteCategory(id);
            if (cat == null)
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
            return Ok(cat);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, string name)
        {
            var cat = await _service.UpdateCategory(id, name);
            if (cat == null)
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
            return Ok(cat);
        }
    }
}
