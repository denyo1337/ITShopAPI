using Application.DTOs.Enums;
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
        public void Seed()
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
                if (!_context.Users.Any())
                {
                    var users = CreateUsersWithAddresses();
                    _context.Users.AddRange(users);
                    _context.SaveChanges();
                }
                if (!_context.Products.Any())
                {
                    var products = CreateProducts();
                    _context.Products.AddRange(products);
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
        private IEnumerable<User> CreateUsersWithAddresses()
        {
            List<User> users = new()
            {
                new User()
                {
                    Name = "Karol",
                    SurrName = "Karolewski",
                    NickName = "Karol",
                    Email = "karol@gmail.com",
                    RoleId = 2,
                    BirthDay = DateTime.Now,
                    Created = DateTime.Now,
                    IsBanned = false,
                    Nationality = "Polska",
                    PhoneNumber = 555333555,
                    IsActive = true,
                    Addresses = new List<Address>(){
                        new Address()
                            {
                                Towm = "Olsztyn",
                                Street = "Prosta 33",
                                PostalCode = "10-109",
                                Country = "Polska",
                                Created = DateTime.Now
                            },
                          new Address()
                            {
                                Towm = "Barczewo",
                                Street = "Krzywa 33",
                                PostalCode = "11-109",
                                Country = "Polska",
                                Created = DateTime.Now
                            }
                    }
                },
                new User()
                {
                    Name = "Marek",
                    SurrName = "Karolewski",
                    NickName = "marek",
                    Email = "marek@gmail.com",
                    RoleId = 1,
                    BirthDay = DateTime.Now,
                    Created = DateTime.Now,
                    IsBanned = false,
                    Nationality = "Polska",
                    PhoneNumber = 555333555,
                    IsActive = true,
                    Addresses = new List<Address>(){
                        new Address()
                            {
                                Towm = "Warszawa",
                                Street = "Skośna 33",
                                PostalCode = "90-119",
                                Country = "Polska",
                                Created = DateTime.Now
                            },
                          new Address()
                            {
                                Towm = "Barczewo",
                                Street = "Krzywa 33",
                                PostalCode = "11-109",
                                Country = "Polska",
                                Created = DateTime.Now
                            }
                    }
                }
            };

            foreach (var user in users)
            {
                var hash = _passwordHasher.HashPassword(user, "password");
                user.PasswordHash = hash;
            }

            return users;
        }
        private IEnumerable<Product> CreateProducts()
        {
            List<Product> products = new()
            {
                new Product()
                {
                    Name = "Procesor",
                    Description = "Procesor komputerowy",
                    Amount = 100,
                    Price = 1300.30,
                    ProductType = ProductTypeEnum.Hardware.ToString(),
                    Created = DateTime.Now.ToLocalTime(),
                    Modified = DateTime.Now.ToLocalTime(),
                },
                new Product()
                {
                    Name = "Karta graficzna",
                    Description = "Karta graficzna",
                    Amount = 20,
                    Price = 300.50,
                    ProductType = ProductTypeEnum.Hardware.ToString(),
                    Created = DateTime.Now.ToLocalTime(),
                    Modified = DateTime.Now.ToLocalTime(),
                },
                new Product()
                {
                    Name = "Monitor",
                    Description = "Monitor komputerowy",
                    Amount = 50,
                    Price = 499.99,
                    ProductType = ProductTypeEnum.Hardware.ToString(),
                    Created = DateTime.Now.ToLocalTime(),
                    Modified = DateTime.Now.ToLocalTime(),
                },
                new Product()
                {
                    Name = "AntyVirus",
                    Description = "Program antiwirusowy",
                    Amount = 200,
                    Price = 30,
                    ProductType = ProductTypeEnum.Software.ToString(),
                    Created = DateTime.Now.ToLocalTime(),
                    Modified = DateTime.Now.ToLocalTime(),
                }
                
            };
            return products;
        }

    }
}
