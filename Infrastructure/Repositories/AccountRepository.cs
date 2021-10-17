using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ITShopDbContext _context;
        public AccountRepository(ITShopDbContext context)
        {
            _context = context;
        }
        public async Task AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();  
        }

        public async Task<User> FindUserByNickOrEmail(string data)
        { 
            return await _context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Email == data || x.NickName == data);
        }

        public async Task<IEnumerable<Address>> GetMyAddresses(int id)
        {
            var list = await _context.Addresses
                .Where(x => x.UserId == id)
                .ToListAsync();
            return list.OrderByDescending(x=>x.Created);
        }
        public async Task<Address> GetAddress(int userId,int addressId )
        {
            return await _context.Addresses
                .Where(x => x.UserId == userId)
                .FirstOrDefaultAsync(x => x.Id == addressId);
        }

        public async Task<User> GetMyDetails(int id)
        {
            return await _context.Users.
                FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> IsNickTaken(string nick)
        {
            return await _context.Users.
                AnyAsync(x => x.NickName == nick);
        }

        public async Task<Address> AddAddress(Address address, int userId)
        {
            address.UserId = userId;
            address.Modified = DateTime.Now.ToLocalTime();
            address.Created = DateTime.Now.ToLocalTime();
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
            return address;
        }

        public async Task UpdateAccountDetails(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task SetIsActiveToFalse(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            user.IsActive = false;
            await _context.SaveChangesAsync();
        }
    }
}
