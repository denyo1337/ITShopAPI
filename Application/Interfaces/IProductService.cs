using Application.DTOs.ProductDtos.ProductDto;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<int> AddProduct(ProductDto dto);
        Task UpdateProduct(int productId, UpdateProductDto dto);
        Task<ProductDto> GetProductById(int productId);
        Task<PageResult<ProductDto>> GetProducts(ProductsQuery query);
        Task DeleteProduct(int productId);
    }
}
