﻿using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("getusers")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _service.GetUsers());
        }
        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            return Ok(await _service.GetUsers(id));
        }
    }
}
