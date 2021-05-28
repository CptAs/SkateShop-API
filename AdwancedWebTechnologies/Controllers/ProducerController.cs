using AdvancedWebTechnologies.Data;
using AdvancedWebTechnologies.Entities;
using AdvancedWebTechnologies.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using AdvancedWebTechnologies.Interfaces;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdvancedWebTechnologies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private readonly IProducerService _service;
        public ProducerController(IProducerService services)
        {
            _service = services;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateProducer([FromBody] string name)
        {
            var producer = await _service.CreateAsync(name);
            return CreatedAtAction(nameof(GetProducerById), new { Id = producer.ProducerId }, producer);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducerById(int id)
        {
            var producer = await _service.GetByIdAsync(id);
            if (producer == null)
            {
                var problem = new ProblemDetails
                {
                    Instance = HttpContext.Request.Path,
                    Status = StatusCodes.Status404NotFound,
                    Type = $"https://httpstatuses.com/404",
                    Title = "Not found",
                    Detail = $"Producer {id} does not exist or you do not have access to it."
                };

                return NotFound(problem);
            }
            return Ok(producer);
        }
        [HttpGet]
        public async Task<IActionResult> GetProducers()
        {
            return Ok(await _service.GetProducers());
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProducer(int id)
        {
            var prod = await _service.DeleteProducerAsync(id);
            if (prod == null)
            {
                var problem = new ProblemDetails
                {
                    Instance = HttpContext.Request.Path,
                    Status = StatusCodes.Status404NotFound,
                    Type = $"https://httpstatuses.com/404",
                    Title = "Not found",
                    Detail = $"Producer {id} does not exist or you do not have access to it."
                };

                return NotFound(problem);
            }
            return Ok(prod);
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateProducer(int id, [FromBody] string name)
        {
            var prod = await _service.UpdateProducerAsync(id, name);
            if (prod == null)
            {
                var problem = new ProblemDetails
                {
                    Instance = HttpContext.Request.Path,
                    Status = StatusCodes.Status404NotFound,
                    Type = $"https://httpstatuses.com/404",
                    Title = "Not found",
                    Detail = $"Producer {id} does not exist or you do not have access to it."
                };

                return NotFound(problem);
            }
            return Ok(prod);
        }
    }
}
