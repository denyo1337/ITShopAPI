using Application.DTOs.OrderDtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;
        public OrderController(IOrderService repository)
        {
            _service = repository;
        }
        [HttpGet("{orderId}")]
        [Authorize(Roles ="Admin, Employee")]
        public async Task<IActionResult> GetOrderById([FromRoute] int orderId)
        {
            var order = await _service.GetOrder(orderId);
            return Ok(order);
        }
        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> CreateOrder([FromBody]CreateOrderDto dto)
        {
            var orderId = await _service.AddOrder(dto);
            return Created($"api/orders/{orderId}",null);
        }
    }
}
