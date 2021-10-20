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
    public class OrderDto : IMap
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Address Address { get; set; }
        public OrderAmountProducts OrderDetails { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime DateOrdered { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderDto>().ReverseMap();
        }
    }
}
