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
            var user = await _context.Users
                .Include(x=>x.Role)
                .FirstOrDefaultAsync(x => x.Email == data || x.NickName == data);
            return user;
        }

        public async Task<bool> IsNickTaken(string nick)
        {
            var isTaken = await _context.Users.AnyAsync(x => x.NickName == nick);
            return isTaken;
        }

    }
}
