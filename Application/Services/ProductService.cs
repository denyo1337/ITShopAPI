using Application.DTOs.ProductDtos.ProductDto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        public ProductService(IMapper mapper, IUserContextService userContextService, IProductRepository repository)
        {
            _mapper = mapper;
            _userContextService = userContextService;
            _repository = repository;
        }

        public async Task<int> AddProduct(ProductDto dto)
        {
            Product newProduct = new()
            {
                Name = dto.Name,
                Description = dto.Description,
                Amount = dto.Amount,
                Price = dto.Price,
                Created = DateTime.Now.ToLocalTime(),
                Modified = DateTime.Now.ToLocalTime(),
            };

            return await _repository.AddNewProduct(newProduct);
        }
    }
}
