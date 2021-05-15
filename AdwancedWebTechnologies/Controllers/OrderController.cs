using AdvancedWebTechnologies.Entities;
using AdvancedWebTechnologies.Interfaces;
using AdvancedWebTechnologies.Models;
using AdvancedWebTechnologies.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvancedWebTechnologies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly UserManager<User> userManager;
        public OrderController(IOrderService service, UserManager<User> user)
        {
            userManager = user;
            orderService = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            return Ok(await orderService.GetOrders());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await orderService.GetOrderById(id);
            if(order!=null)
            {
                return Ok(order);
            }
            else
            {
                var problem = new ProblemDetails
                {
                    Instance = HttpContext.Request.Path,
                    Status = StatusCodes.Status404NotFound,
                    Type = $"https://httpstatuses.com/404",
                    Title = "Not found",
                    Detail = $"Order {id} does not exist."
                };

                return NotFound(problem);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await orderService.DeleteOrder(id);
            if (order != null)
            {
                return Ok(order);
            }
            else
            {
                var problem = new ProblemDetails
                {
                    Instance = HttpContext.Request.Path,
                    Status = StatusCodes.Status404NotFound,
                    Type = $"https://httpstatuses.com/404",
                    Title = "Not found",
                    Detail = $"Order {id} does not exist."
                };

                return NotFound(problem);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody]Status status)
        {
            var order = await orderService.UpdateOrder(id, status);
            if (order != null)
            {
                return Ok(order);
            }
            else
            {
                var problem = new ProblemDetails
                {
                    Instance = HttpContext.Request.Path,
                    Status = StatusCodes.Status404NotFound,
                    Type = $"https://httpstatuses.com/404",
                    Title = "Not found",
                    Detail = $"Order {id} does not exist."
                };

                return NotFound(problem);
            }
        }
        [HttpDelete("orderProduct/{id}")]
        public async Task<IActionResult> DeleteOrderProduct(int id)
        {
            var orderProduct = await orderService.DeleteOrderProduct(id);
            if (orderProduct != null)
            {
                return Ok(orderProduct);
            }
            else
            {
                var problem = new ProblemDetails
                {
                    Instance = HttpContext.Request.Path,
                    Status = StatusCodes.Status404NotFound,
                    Type = $"https://httpstatuses.com/404",
                    Title = "Not found",
                    Detail = $"Order {id} does not exist."
                };

                return NotFound(problem);
            }
        }
        [HttpPut("orderProduct/{id}")]
        public async Task<IActionResult> UpdateOrderProduct(int id, [FromBody] int quantity)
        {
            var orderProduct = await orderService.UpdateOrderProduct(id, quantity);
            if (orderProduct != null)
            {
                return Ok(orderProduct);
            }
            else
            {
                var problem = new ProblemDetails
                {
                    Instance = HttpContext.Request.Path,
                    Status = StatusCodes.Status404NotFound,
                    Type = $"https://httpstatuses.com/404",
                    Title = "Not found",
                    Detail = $"Order {id} does not exist."
                };

                return NotFound(problem);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var order = await orderService.CreateOrder(user);
            return CreatedAtAction(nameof(GetOrderById), new { Id = order.OrderId }, order);

        }
        [HttpGet("orderProduct/{id}")]
        public async Task<IActionResult> GetOrderProductById(int id)
        {
            var orderProduct = await orderService.GetOrderProductById(id);
            return Ok(orderProduct);
        }
        [HttpPost("orderProduct")]
        public async Task<IActionResult> CreateOrderProduct([FromBody] CreateOrderDto orderDto)
        {
            var orderProduct = await orderService.CreateOrderProduct(orderDto.Quantity, orderDto.OrderId, orderDto.ProductId);
            return CreatedAtAction(nameof(GetOrderProductById), new { Id = orderProduct.OrderProductID }, orderProduct);
        }


    }
}
