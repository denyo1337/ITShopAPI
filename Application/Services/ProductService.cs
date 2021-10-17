using Application.Interfaces;
using AutoMapper;
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

    }
}
