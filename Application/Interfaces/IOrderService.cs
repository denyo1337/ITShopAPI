using Application.DTOs.OrderDtos;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        Task<int> AddOrder(CreateOrderDto dto);
        Task<OrderDto> GetOrder(int orderId);
        Task<IEnumerable<ListOfOrdersDto>> GetOrdersList(OrdersQuery query);
    }
}
