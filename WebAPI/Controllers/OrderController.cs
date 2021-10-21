using Application.DTOs.OrderDtos;
using Application.Interfaces;
using Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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
        [SwaggerOperation(Summary = "Pracownik lub administrator może zobaczyć zamówienie o konkretnym orderId")]

        public async Task<IActionResult> GetOrderById([FromRoute] int orderId)
        {
            var order = await _service.GetOrder(orderId);
            return Ok(order);
        }
        [HttpGet]
        [Authorize(Roles ="Admin, Employee")]
        [SwaggerOperation(Summary = "Pracownik lub admin moze zobaczyć liste zamówień")]
        public async Task<IActionResult> GetOrders([FromQuery] OrdersQuery query)
        {
            var orders = await _service.GetOrdersList(query);
            return Ok(orders);
        }

        [HttpPost("create")]
        [Authorize]
        [SwaggerOperation(Summary = "Admin może pobrać listę wszystkich użytkowników i ich adresów.")]
        public async Task<IActionResult> CreateOrder([FromBody]CreateOrderDto dto)
        {
            var orderId = await _service.AddOrder(dto);
            return Created($"api/orders/{orderId}",null);
        }
        
    }
}
