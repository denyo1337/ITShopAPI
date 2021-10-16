using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI
{
    public class Seeder
    {
        private readonly ITShopDbContext _context;
        private IPasswordHasher<User> _passwordHasher { get; }
        public Seeder(ITShopDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }
        public void SeedUsers()
        {
            if (_context.Database.CanConnect())
{
                var pendingMigrations = _context.Database.GetPendingMigrations();
                if (pendingMigrations != null && pendingMigrations.Any())
                {
                    _context.Database.Migrate();
                }
                if (!_context.Roles.Any())
                {
                    var roles = GetRoles();
                    _context.Roles.AddRange(roles);
                    _context.SaveChanges();
                }
                var admin = _context.Users.FirstOrDefault(x => x.Email == "admin@gmail.com");
                if(admin == null)
                {
                    CreateAdmin();
                }
            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name="User",
                    HasPrivileges = false
                },
                new Role()
                {
                    Name="Employee",
                    HasPrivileges = false
                },
                new Role()
                {
                    Name="Admin",
                    HasPrivileges = true
                }
            };
            return roles;
        }
        private void CreateAdmin()
        {
            var newAdmin = new User()
            {
                Name = "Damian",
                SurrName = "Grabowski",
                NickName = "admin",
                Email = "admin@gmail.com",
                RoleId = 3
            };
            var password = "password";
            var hashedPassword = _passwordHasher.HashPassword(newAdmin, password);
            newAdmin.PasswordHash = hashedPassword;
            _context.Users.Add(newAdmin);
            _context.SaveChanges();
        }
      
    }
}
