using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Product>> GetProductAmount();
        Task UpdateStates(Order order, IEnumerable<Product> product );
    }
}
