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
    public class AdminRepository : IAdminRepository
    {
        private readonly ITShopDbContext _context;
        public AdminRepository(ITShopDbContext context)
        {
            _context = context;
        }

        public async Task ChangeRoleForUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }

        public async Task DeleteUser(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUser(int id)
        {
            return await _context.Users
                .Include(x => x.Role)
                .Include(x => x.Addresses)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users
                .Include(x => x.Role)
                .Include(x=>x.Addresses)
                .ToListAsync();
        }

        public async Task SetIsBanned(User user, bool flag)
        {
            user.IsBanned = flag;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
