using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.OrderDtos
{
    public class ListOfOrdersDto : IMap
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime DateOrdered { get; set; }
        public decimal TotalCost { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ListOfOrdersDto, Order>()
                .ReverseMap()
                .ForMember(x=>x.OrderId, s=>s.MapFrom(y=>y.Id));
        }
    }
}
