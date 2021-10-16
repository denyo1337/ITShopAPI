using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task AddUser(User user);
        Task<bool> IsNickTaken(string nickl);
        Task<User> FindUserByNickOrEmail(string data);
    }
}
