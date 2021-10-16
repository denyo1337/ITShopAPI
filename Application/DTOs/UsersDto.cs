using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UsersDto : IMap
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string NickName { get; set; }
        public string Name { get; set; }
        public string SurrName { get; set; }
        public int? PhoneNumber { get; set; }
        public string Nationality { get; set; }
        public DateTime BirthDay { get; set; }
        public bool IsBanned { get; set; }

       [JsonPropertyName("role")]
        public string RoleName { get; set; }

        public IEnumerable<Address> Addresses { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UsersDto, User>()
                .ReverseMap()
                .ForMember(x => x.RoleName, y => y.MapFrom(c => c.Role.Name));
        }
    }
}
