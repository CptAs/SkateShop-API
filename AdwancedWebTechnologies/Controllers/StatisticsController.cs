using AdvancedWebTechnologies.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvancedWebTechnologies.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStaticticsService _service;
        
        public StatisticsController(IStaticticsService service)
        {
            _service = service;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("month/last")]
        public async Task<IActionResult> GetStatisticsFromLastMonth()
        {
            return Ok(await _service.GetStatisticsFromLastMonth());
        }
    }
}
