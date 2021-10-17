using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
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
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUserContextService _userContextService;
        public AdminService(IAdminRepository repository, IMapper mapper, IPasswordHasher<User> passwordHasher, IUserContextService userContextService)
        {
            _repository = repository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _userContextService = userContextService;
        }

        public async Task BanOrUnbanUser(int id, bool flag)
        {
            var myId = _userContextService.GetUserId;
            if (myId == id)
            {
                throw new BadRequestException("Nie możesz zbanować samego siebie");
            }
            var user = await _repository.GetUser(id);
            if (user == null)
                throw new UserNotFoundException($"Użytkownik o id {id} nie istnieje");
            if (user.IsBanned == true && flag == true)
                throw new BadFormatException("Użytkownik jest już zablokowany");
            if (user.IsBanned == false && flag == false)
                throw new BadFormatException("Też użytkownik nie jest zablokowany");

            await _repository.SetIsBanned(user, flag);
        }

        public async Task<int> CreateUser(CreateUserDto dto)
        {
            var newUser = new User()
            {
                Email = dto.Email,
                Name = dto.Name,
                Created = DateTime.Now.ToLocalTime(),
                IsActive = dto.IsActive,
                IsBanned = dto.IsBanned,
                Modified = DateTime.Now.ToLocalTime(),
                Nationality = dto.Nationality,
                PhoneNumber = dto.PhoneNumber,
                RoleId = dto.RoleId,
                SurrName = dto.SurrName,
                NickName = dto.NickName,
            };
            var hasedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
            newUser.PasswordHash = hasedPassword;
            var id = await _repository.CreateUser(newUser);

            return id;
        }

        public async Task DeleteUser(int id)
        {
            var myId = _userContextService.GetUserId;
            if (myId == id)
            {
                throw new BadRequestException("Nie możesz zbanować samego siebie");
            }
            var user = await _repository.GetUser(id);
            if (user == null)
            {
                throw new UserNotFoundException($"Użytkownik o id:{id} nie istnieje");
            }
            await _repository.DeleteUser(user);
        }

        public async Task<IEnumerable<UsersDto>> GetUsers()
        {
            var users = await _repository.GetUsers();
            if(users == null)
            {
                throw new EmptyListException("Lista użytkowników jest pusta");
            }

            return _mapper.Map<IEnumerable<UsersDto>>(users);
        }

        public async Task<UsersDto> GetUsers(int id)
        {
            var user = await _repository.GetUser(id);
            if (user == null)
            {
                throw new UserNotFoundException($"Użytkownik o id:{id} nie istnieje");
            }
            return _mapper.Map<UsersDto>(user);
        }

        public async Task PromoteUser(int id, int roleId)
        {
            var user =  await _repository.GetUser(id);
            if (user == null)
            {
                throw new UserNotFoundException($"Użytkownik o id:{id} nie istnieje");
            }
            if(roleId > 3 || roleId < 1)
            {
                throw new BadFormatException("RoleId mieści się między 1-3, gdzie 1-User, 2-Employee, 3-Admin");
            }
            user.RoleId = roleId;
            await _repository.ChangeRoleForUser(user);
        }
    }
}
