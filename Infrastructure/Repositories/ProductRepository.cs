using Application.DTOs.ProductDtos;
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

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ITShopDbContext _context;
        public ProductRepository(ITShopDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddNewProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }

        public async Task DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> GetProductbyId(int productId)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
        }

        public async Task<IEnumerable<Product>> GetProducts(ProductsQuery query )
        {
            return await _context.Products
                .Where(x => query.SearchPhrase == null || (x.Name.ToLower().Contains(query.SearchPhrase.ToLower()) || x.Description.ToLower().Contains(query.SearchPhrase.ToLower())))
                .ToListAsync();
        }

        public async Task UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
