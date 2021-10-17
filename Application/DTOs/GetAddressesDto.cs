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
    public class GetAddressesDto : IMap
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string Towm { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Created { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Address, GetAddressesDto>()
                .ForMember(x => x.Created, s => s.MapFrom(y => y.Created.ToString("dd/MM/yyyy HH:mm:ss")));
        }
    }
}
