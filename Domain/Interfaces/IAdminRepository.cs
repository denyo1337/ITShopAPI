using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAdminRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int id);
        Task<int> CreateUser(User user);
        Task SetIsBanned(User user, bool flag);
        Task ChangeRoleForUser(User user);
        Task DeleteUser(User user);
    }
}
