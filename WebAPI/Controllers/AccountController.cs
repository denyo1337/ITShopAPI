using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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
        [SwaggerOperation(Summary = "Umożliwia sprawdzanie danych konta po zalogowaniu")]
        public async Task<IActionResult> MyAccountDetails()
        {
            return Ok(await _service.GetMyAccountDetails());
        }
        [HttpGet("details/addresses")]
        [Authorize]
        [SwaggerOperation(Summary = "Pobiera liste adresów dodanych do mojego konta, posortowane od najnowszego dodanego adresu.")]
        public async Task<IActionResult> GetMyAddresses()
        {
            return Ok(await _service.GetAddresses());
        }
        [HttpGet("details/addresses/{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "Pobiera adres po sprecyzowaniu id, wyszkuje wyłącznie adresy zalogowanego użytkownika.")]
        public async Task<IActionResult> GetAddressById([FromRoute] int id)
        {
            return Ok(await _service.GetMyAddressById(id));
        }

        [HttpPost("sign-in")]
        [SwaggerOperation(Summary = "Logowanie")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
        {
            return Ok(await _service.GenerateJWT(dto));
        }
        [HttpPost("sign-up")]
        [SwaggerOperation(Summary = "Rejestracja")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            await _service.RegisterUser(dto);
            return Ok();
        }
        [HttpPost("details/addresses/add")]
        [Authorize]
        [SwaggerOperation(Summary = "Dodawanie adresu do zalogowanego konta.")]
        public async Task<IActionResult> AddAddress([FromBody] AddressDto dto)
        {
            var addresId = await _service.AddAddress(dto);
            return Created($"/api/details/addresses/{addresId}", null);
        }
        [HttpPut]
        [Authorize]
        [SwaggerOperation(Summary = "Umożliwia edycje konta po zalogowaniu.")]
        public async Task<IActionResult> EditMyDetails([FromBody] ProfileDetailsDto dto)
        {
            await _service.EditProfileDetails(dto);
            return NoContent();
        }
        [HttpDelete]
        [Authorize]
        [SwaggerOperation(Summary ="Umożliwia 'usunięcie konta', zmienia status konta isActive=false")]
        public async Task<IActionResult> DeleteMyAccount([FromBody] DeactivateAccountDto dto )
        {
            await _service.DeactivateAccount(dto);
            return NoContent();
        }
    }
}
