using Application.DTOs.ProductDtos.ProductDto;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpPost("add")]
        [SwaggerOperation(Summary = "Umożliwia dodanie produktu, role = [Admin,Employee]")]
        [Authorize(Roles ="Admin,Employee")]
        public async Task<IActionResult> AddProduct([FromBody] ProductDto dto)
        {
            var id = await _service.AddProduct(dto);

            return Created($"api/product/{id}", null);
        }
    }
}
