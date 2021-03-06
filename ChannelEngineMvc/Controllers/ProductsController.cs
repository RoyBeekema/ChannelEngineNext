﻿using ChannelEngineMvc.Models;
using MerchantDomain;
using MerchantLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ChannelEngineMvc.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IBaseProductRepository _repository;

        public ProductsController(IBaseProductRepository repository, ILogger<ProductsController> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            await _repository.Sync();
            var model = GetProducts();

            return View(model);
        }

        [HttpPost]
        public async Task<PartialViewResult> Post(string merchantProductNo, string property, int value)
        {
            await _repository.Set(merchantProductNo, property, value);
            await _repository.Sync();
            var model = GetProducts();

            return PartialView("Index",model);
        }

        [HttpPatch]
        public async Task<IActionResult> Patch(string merchantProductNo, string property, int value)
        {
            await _repository.Set(merchantProductNo, property, value);
            await _repository.Sync();
            var model = GetProducts();

            return Json(model);
        }
 
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var reqId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogError(reqId);
            return View(new ErrorViewModel { RequestId = reqId });
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
