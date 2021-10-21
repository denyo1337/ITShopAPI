using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ITShopDbContext _context;
        public OrderRepository(ITShopDbContext context)
        {
            _context = context;
        }

        public Task<Order> GetOrder(int orderId)
        {
            return _context.Orders
                .Include(x => x.OrderAmountProducts)
                .Include(x => x.Address)
                .FirstOrDefaultAsync(x => x.Id == orderId);
        }

        public async Task<IEnumerable<Order>> GetOrders(OrdersQuery query)
        {
            return await _context.Orders
                .Include(x => x.OrderAmountProducts)
                .ToListAsync(); 
        }

        public async Task<IEnumerable<Product>> GetProductAmount()
        {
            return await _context.Products.ToListAsync();
}

        public async Task UpdateStates(Order order, IEnumerable<Product> product)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }
    }
}
