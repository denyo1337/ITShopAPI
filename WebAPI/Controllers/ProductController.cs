using Application.DTOs.ProductDtos;
using Application.DTOs.ProductDtos.ProductDto;
using Application.Interfaces;
using Domain.Common;
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
        [HttpGet("{productId}")]
        [SwaggerOperation(Summary = "Umożliwia pobranie produktu po productId")]
        public async Task<IActionResult> GetProductbyId([FromRoute] int productId)
        {
            var product = await _service.GetProductById(productId);
            return Ok(product);
        }
        [HttpGet]
        [SwaggerOperation(Summary = "Umożliwia pobranie wszystkich produktów, uwzględniajać paginację")]
        public async Task<IActionResult> GetProducts([FromQuery] ProductsQuery query)
        {
            var products = await _service.GetProducts(query);
            return Ok(products);
        }
        [HttpPost("add")]
        [SwaggerOperation(Summary = "Umożliwia dodanie produktu, role = [Admin,Employee]")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> AddProduct([FromBody] ProductDto dto)
        {
            var id = await _service.AddProduct(dto);

            return Created($"api/product/{id}", null);
        }
        [HttpPut("{productId}/update")]
        [SwaggerOperation(Summary = "Umożliwia aktualizacje produktu, role = [Admin,Employee]")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int productId, [FromBody] UpdateProductDto dto)
        {
            await _service.UpdateProduct(productId, dto);
            return NoContent();
        }
        [HttpDelete("{productId}/delete")]
        [SwaggerOperation(Summary = "Umożliwia aktualizacje produktu, role = [Admin,Employee]")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> DeleteProductById([FromRoute] int productId)
        {
            await _service.DeleteProduct(productId);
            return NoContent();
        }



    }
}
