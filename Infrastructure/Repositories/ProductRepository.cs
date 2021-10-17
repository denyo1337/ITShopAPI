using Domain.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ITShopDbContext _context;
        public ProductRepository(ITShopDbContext context)
        {
            _context = context;
        }
    }
}
