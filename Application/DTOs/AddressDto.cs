using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class AddressDto : IMap
    {
        public string Street { get; set; }
        public string Towm { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddressDto, Address>()
                .ReverseMap();
        }
    }
}
