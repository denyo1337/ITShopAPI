using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;
        public AccountController(IAccountService service)
        {
            _service = service;
        }
        [HttpGet("details")]
        [Authorize]
        public async Task<IActionResult> MyAccountDetails()
        {
            return Ok(await _service.GetMyAccountDetails());
        }
        [HttpGet("details/addresses")]
        [Authorize]
        public async Task<IActionResult> GetMyAddresses()
        {
            return Ok(await _service.GetAddresses());
        }
        [HttpGet("details/addresses/{id}")]
        [Authorize]
        public async Task<IActionResult> GetAddressById([FromRoute] int id)
        {
            return Ok(await _service.GetMyAddressById(id));
        }

        [HttpPost("signin")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
        {
            return Ok(await _service.GenerateJWT(dto));
        }
        [HttpPost("signup")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            await _service.RegisterUser(dto);
            return Ok();
        }
        [HttpPost("details/addresses/add")]
        [Authorize]
        public async Task<IActionResult> AddAddress([FromBody] AddressDto dto)
        {
            var addresId = await _service.AddAddress(dto);
            return Created($"/api/details/addresses/{addresId}", null);
        }
    }
}
