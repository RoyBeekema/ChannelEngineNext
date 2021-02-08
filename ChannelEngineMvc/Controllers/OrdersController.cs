using ChannelEngineMvc.Models;
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
    public class OrdersController : Controller
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IBaseOrderRepository _repository;

        public OrdersController(IBaseOrderRepository repository, ILogger<OrdersController> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            await _repository.Sync();
            var model = GetOrders();

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var reqId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogError(reqId);
            return View(new ErrorViewModel { RequestId = reqId });
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
