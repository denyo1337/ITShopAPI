using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepository<User> _repository;
        private readonly IPasswordHasher<User> _passwordHasher;
        public AccountService(IRepository<User> repository, IPasswordHasher<User> passwordHasher)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;

        }
        public async Task RegisterUser(RegisterUserDto dto)
        {
            User newUser = new()
            {
                Email = dto.Email,
                Name = dto.Name,
                SurrName = dto.SurrName,
                PhoneNumber = dto.PhoneNumber,
                Nationality = dto.Nationality,
                BirthDay = dto.BirthDay,
                Created = DateTime.Now.ToLocalTime(),
                RoleId = 1,
                NickName = dto.Email.Split("@")[0]
            };
            var passwordHash = _passwordHasher.HashPassword(newUser, dto.Password);
            newUser.PasswordHash = passwordHash;

            await _repository.Add(newUser);
        }
    }
}
