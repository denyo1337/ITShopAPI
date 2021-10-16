using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        public AccountService(IAccountRepository repository, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;

        }

        public async Task<string> GenerateJWT(LoginUserDto dto)
        {
            var user = await _repository.FindUserByNickOrEmail(dto.Login);
            if(user == null)
            {
                throw new UserNotFoundException("Podałeś zły login lub hasło");
            }
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if(result == PasswordVerificationResult.Failed)
            {
                throw new UserNotFoundException("Podałeś zły login lub hasło");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.UserData, user.NickName),
                new Claim(ClaimTypes.Role, user.Role.Name),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
               _authenticationSettings.JwtIssuer,
               claims,
               expires: expires,
               signingCredentials: cred);
            var tokenHandler = new JwtSecurityTokenHandler();


            return tokenHandler.WriteToken(token);
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
                NickName = dto.NickName,
                Created = DateTime.Now.ToLocalTime(),
                RoleId = 1,
                IsBanned = false
            };

            var passwordHash = _passwordHasher.HashPassword(newUser, dto.Password);
            newUser.PasswordHash = passwordHash;
            await _repository.AddUser(newUser);
        }
        
    }
}
