using MerchantDomain;
using MerchantLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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
            try
            {
                await _repository.Sync();

                return GetProducts();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        [HttpPatch("{merchantProductNo}/{property}/{value}")]
        public async Task<IEnumerable<ProductModel>> Patch(string merchantProductNo, string property, int value)
        {
            try
            {
                await _repository.Set(merchantProductNo, property, value);
                await _repository.Sync();

                return GetProducts();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
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
