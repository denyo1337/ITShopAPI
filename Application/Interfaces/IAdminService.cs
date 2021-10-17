using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<UsersDto>> GetUsers();
        Task<UsersDto> GetUsers(int id);
        Task<int> CreateUser(CreateUserDto dto);
        Task BanOrUnbanUser(int id, bool flag);
        Task PromoteUser(int id, int roleId);
        Task DeleteUser(int id);
    }
}
