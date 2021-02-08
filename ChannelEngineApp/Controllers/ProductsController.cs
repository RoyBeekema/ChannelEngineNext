﻿using MerchantDomain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChannelEngineApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IBaseProductRepository _repository;

        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IBaseProductRepository repository, ILogger<ProductsController> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductModel>> Get()
        {
            await _repository.Sync();

            return GetProducts();
        }

        [HttpPatch("{merchantProductNo}/{property}/{value}")]
        public async Task<IEnumerable<ProductModel>> Patch(string merchantProductNo, string property, int value)
        {
            await _repository.Set(merchantProductNo, property, value);
            await _repository.Sync();

            return GetProducts();
        }

        private IEnumerable<ProductModel> GetProducts()
        {
            // TODO[r.beekema]: Refactor to use mapping
            return _repository.Products.Select(product => new ProductModel
            {
                Brand = product.Brand,
                Description = product.Description,
                Name = product.Name,
                Stock = product.Stock,
                MerchantProductNo = product.MerchantProductNo
            });
        }
    }
}