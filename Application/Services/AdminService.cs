using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
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
        public AdminService(IAdminRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
    }
}
