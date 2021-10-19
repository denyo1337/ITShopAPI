using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<int> AddNewProduct(Product product);
        Task<Product> GetProductbyId(int productId);
        Task UpdateProduct(Product product);
        Task DeleteProduct(Product product);
        Task<IEnumerable<Product>> GetProducts(ProductsQuery query);
    }
}
