using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
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
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;
        public AccountService(IAccountRepository repository, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings, IUserContextService userContextService, IMapper mapper)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
            _userContextService = userContextService;
            _mapper = mapper;
        }

        public async Task<string> GenerateJWT(LoginUserDto dto)
        {
            var user = await _repository.FindUserByNickOrEmail(dto.Login);
            if(user == null)
            {
                throw new UserNotFoundException("Podałeś zły login lub hasło");
            }
            if (user.IsBanned == true)
            {
                throw new BannedAccountException("Twoje konto zostało zablokowane");
            }
            if(user.IsActive == false)
            {
                throw new BannedAccountException("Twoje konto nie istnieje lub jest nieaktywne");
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
                IsBanned = false,
                IsActive = true
            };

            var passwordHash = _passwordHasher.HashPassword(newUser, dto.Password);
            newUser.PasswordHash = passwordHash;
            await _repository.AddUser(newUser);
        }
        public async Task<AccountDetailsDto> GetMyAccountDetails()
        {
            var userId = GetUserId();
            var myuserDetails = await _repository.GetMyDetails(userId);
            return _mapper.Map<AccountDetailsDto>(myuserDetails);
        }

        public async Task<IEnumerable<GetAddressesDto>> GetAddresses()
        {
            var userId = GetUserId();
            var myaddresses = await _repository.GetMyAddresses(userId);
            return _mapper.Map<IEnumerable<GetAddressesDto>>(myaddresses);
        }
        public async Task<GetAddressesDto> GetMyAddressById(int addressId)
        {
            var userId = GetUserId();
            var myaddress = await _repository.GetAddress(userId, addressId);
            var mapped = _mapper.Map<GetAddressesDto>(myaddress);
            return mapped;
        }

        public async Task<int> AddAddress(AddressDto dto)
        {
            var userId = GetUserId();
            var mapped = _mapper.Map<Address>(dto);
            var address = await _repository.AddAddress(mapped, userId);

            return address.Id;
        }

        public async Task EditProfileDetails(ProfileDetailsDto dto)
        {
            var userId = GetUserId();
            var account = await _repository.GetMyDetails(userId);

            if(dto.BirthDay != null)
                account.BirthDay = (DateTime)dto.BirthDay;
            if (!string.IsNullOrWhiteSpace(dto.Name))
                account.Name = dto.Name;
            if (!string.IsNullOrWhiteSpace(dto.SurrName))
                account.SurrName = dto.SurrName;
            if (!string.IsNullOrWhiteSpace(dto.Nationality))
                account.Nationality = dto.Nationality;
            if(!string.IsNullOrWhiteSpace(dto.NickName))
            {
                var isTaken = await _repository.IsNickTaken(dto.NickName);
                if (isTaken)
                    throw new NickNameAlreadyTakenException($"Pseudonim '{dto.NickName}' jest już zajęty");
            }
            if (dto.PhoneNumber != null)
                account.PhoneNumber = dto.PhoneNumber;

            account.Modified = DateTime.Now.ToLocalTime();

            await _repository.UpdateAccountDetails(account);

        }

        public async Task DeactivateAccount(DeactivateAccountDto dto)
        {
            var userid = GetUserId();
            if (dto.Password != dto.ConfirmPassword)
                throw new WrongPasswordException("Hasła nie są identyczne");

            await _repository.SetIsActiveToFalse(userid);
        }
        private int GetUserId()
        {
            return (int)_userContextService.GetUserId;
        }

    }
}
