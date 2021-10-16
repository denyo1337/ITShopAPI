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
    }
}
