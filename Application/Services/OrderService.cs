using Application.DTOs.OrderDtos;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;
        public OrderService(IOrderRepository repository, IMapper mapper, IUserContextService userContextService)
        {
            _repository = repository;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        public async Task<int> AddOrder(CreateOrderDto dto)
        {
            Order newOrder = new()
            {
                UserId = (int)_userContextService.GetUserId,
                DateOrdered = DateTime.Now.ToLocalTime(),
                AddressId = dto.AddressId,
                OrderAmountProducts = new List<OrderAmountProducts>()
            };
            decimal sum = 0;

            var outOfStoreProductsIds = new List<int>();

            var availableProd = await  _repository.GetProductAmount();

            foreach (var item in dto.Products)
            {
                var consider = availableProd.FirstOrDefault(x => x.Id == item.Id);
                if (consider == null)
                    throw new NotFoundException($"Produkt o id {item.Id} nie istnieje");
                if (consider.Amount < item.Amount)
                    outOfStoreProductsIds.Add(item.Id);

                sum += (decimal)consider.Price * item.Amount;
                newOrder.OrderAmountProducts.Add(new OrderAmountProducts
                {
                    ProductId = item.Id,
                    Amount = item.Amount
                });
                consider.Amount -= item.Amount;
               
            }
            if (outOfStoreProductsIds.Count > 0)
                throw new OutOfStoreException($"Produkty o id [{string.Join(", ", outOfStoreProductsIds)}] nie są dostępne w ilości okreslonej przez zamawiającego");

            newOrder.TotalCost = sum;

            await _repository.UpdateStates(newOrder, availableProd);

            return newOrder.Id;
        }

        public async Task<OrderDto> GetOrder(int orderId)
        {
            var order = await _repository.GetOrder(orderId);
            if (order == null)
                throw new NotFoundException($"Zamówienie o id {orderId} nie istnieje");
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<IEnumerable<ListOfOrdersDto>> GetOrdersList(OrdersQuery query)
        {
            var orders = await _repository.GetOrders(query);

            if (orders.Count() == 0)
                throw new NotFoundException("Pusta lista zamówień");

            orders = orders.OrderBy(x => x.DateOrdered);

            var ordersBase =  orders.Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize);


            return _mapper.Map<IEnumerable<ListOfOrdersDto>>(ordersBase);
        }
    }
}


