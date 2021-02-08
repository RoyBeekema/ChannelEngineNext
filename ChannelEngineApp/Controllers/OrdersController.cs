using MerchantDomain;
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
    public class OrdersController : ControllerBase
    {
        private readonly IBaseOrderRepository _repository;

        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IBaseOrderRepository repository, ILogger<OrdersController> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<OrderModel>> Get()
        {
            try
            {
                await _repository.Sync();

                return GetOrders();
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }            
        }

        private IEnumerable<OrderModel> GetOrders()
        {
            // TODO[r.beekema]: Refactor to use mapping
            return _repository.Orders.OrderByDescending(x => x.Lines.First().Quantity).Skip(0).Take(5).Select(order => new OrderModel
            {
                Id = order.Id,
                Description = order.Lines.First().Description,
                Gtin = order.Lines.First().Gtin,
                Quantity = order.Lines.First().Quantity,
                MerchantProductNo = order.Lines.First().MerchantProductNo
            });
        }
    }
}
