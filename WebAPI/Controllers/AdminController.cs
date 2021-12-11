using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
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
    [Authorize(Roles = "Admin" )]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _service; 
        public AdminController(IAdminService service)
        {
            _service = service;
        }

        [HttpGet("users")]
        [SwaggerOperation(Summary = "Admin może pobrać listę wszystkich użytkowników i ich adresów.")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _service.GetUsers());
        }

        [HttpGet("users/{id}")]
        [SwaggerOperation(Summary = "Pobieranie użytkownika po Id")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            return Ok(await _service.GetUsers(id));
        }
        [HttpPost("create-user")]
        [SwaggerOperation(Summary = "Admin może tworzyć użytkowników i nadawać im role.")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
        {
            var userId = await _service.CreateUser(dto);
            return Created($"/api/admin/users/{userId}", null);
        }
        [HttpPut("users/{id}/ban={flag}")]
        [SwaggerOperation(Summary = "Admin może zbanować użytkownika lub odbanować")]
        public async Task<IActionResult> BanUser([FromRoute] int id, [FromRoute] bool flag)
        {
            await _service.BanOrUnbanUser(id, flag);

            return NoContent();
        }
        [HttpPut("users/{id}/set-role/{roleId}")]
        [SwaggerOperation(Summary = "Admin może zmienić role użytkownikowi.")]
        public async Task<IActionResult> UpdateUserRole([FromRoute]int id, [FromRoute] int roleId)
        {
            await _service.PromoteUser(id, roleId);
            return NoContent();
        }
        [HttpDelete("users/{id}/delete")]
        [SwaggerOperation(Summary = "Admin może usunąć użytkownika z bazy danych")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            await _service.DeleteUser(id);
            return NoContent();
        }
    }
}
