using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Mappings;
using AutoMapper;

namespace Application.DTOs
{
    public class CreateUserDto : IMap
    {
        public string Email { get; set; }
        public string NickName { get; set; }
        public string Name { get; set; }
        public string SurrName { get; set; }
        public int? PhoneNumber { get; set; }
        public string Nationality { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public DateTime? BirthDay { get; set; }
        public int RoleId { get; set; }
        public bool IsBanned { get; set; }
        public bool IsActive { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateUserDto, User>();
        }
    }
}
