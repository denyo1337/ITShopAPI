using Application.DTOs;
using Application.Interfaces;
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
        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            await _service.RegisterUser(dto);
            return Ok();
        }
        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
        {
            var key = await _service.GenerateJWT(dto);
            return Ok(key);
        }
    }
}
