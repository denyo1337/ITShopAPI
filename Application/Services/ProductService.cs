using Application.DTOs.Enums;
using Application.DTOs.ProductDtos;
using Application.DTOs.ProductDtos.ProductDto;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
                ProductType = dto.ProductType,
                Created = DateTime.Now.ToLocalTime(),
                Modified = DateTime.Now.ToLocalTime(),
            };

            return await _repository.AddNewProduct(newProduct);
        }
        public async Task<PageResult<ProductDto>> GetProducts(ProductsQuery query)
        {
            var products = await _repository.GetProducts(query);
            if (products == null)
                throw new EmptyListException("Brak produktów do wyświetlenia");

            var totalCount = products.Count();

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelectors = new Dictionary<string, Expression<Func<Product, object>>>
                {
                    {nameof(Product.Name), r=>r.Name },
                    {nameof(Product.Amount), r=>r.Amount },
                    {nameof(Product.Price), r=>r.Price }
                };
                var selectedColumn = columnsSelectors[query.SortBy];

                products = query.SortDirection == SortDirection.ASC ? products.AsQueryable().OrderBy(selectedColumn)
                    : products.AsQueryable().OrderByDescending(selectedColumn);
            }
            else
            {
                products = products.OrderBy(x => x.Name);
            }

            products = products
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize);

            var productDto = _mapper.Map<IEnumerable<ProductDto>>(products);

            return new PageResult<ProductDto>(productDto, totalCount, query.PageSize, query.PageNumber);
        }
        public async Task<ProductDto> GetProductById(int productId)
        {
            var product = await _repository.GetProductbyId(productId);
            if (product == null)
                throw new NotFoundException($"Product o id {productId} nie istnieje");

            return _mapper.Map<ProductDto>(product);
        }

        public async Task UpdateProduct(int productId, UpdateProductDto dto)
        {
            var product = await _repository.GetProductbyId(productId);
            if (product == null)
                throw new NotFoundException($"Product o id {productId} nie istnieje");

            if (!string.IsNullOrWhiteSpace(dto.Description))
                product.Description = dto.Description;
            if (!string.IsNullOrWhiteSpace(dto.Name))
                product.Name = dto.Name;
            if (dto.Price != null)
                product.Price = dto.Price;
            if (dto.Amount != null)
                product.Amount = dto.Amount;

            product.Modified = DateTime.Now.ToLocalTime();

            await _repository.UpdateProduct(product);
        }

        public async Task DeleteProduct(int productId)
        {
            var product = await _repository.GetProductbyId(productId);
            if (product == null)
                throw new NotFoundException($"Product o id {productId} nie istnieje");
            await _repository.DeleteProduct(product);
        }
    }
}
