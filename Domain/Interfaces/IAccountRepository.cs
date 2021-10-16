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
        Task<User> GetMyDetails(int id);
        Task<IEnumerable<Address>> GetMyAddresses(int id);
        Task<Address> GetAddress(int userId, int addressId);
    }
}
