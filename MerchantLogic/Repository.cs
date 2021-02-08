using MerchantData;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;

namespace MerchantLogic
{
    public class Repository
    {
        private static OrderRepository _orderRepository;
        private static ProductRepository _productsRepository;

        public Repository(IHttpClientFactory clientFactory,ILogger<OrderRepository> orderLogger, ILogger<ProductRepository> productLogger)
        {
            _orderRepository = new OrderRepository(clientFactory, orderLogger);
            _productsRepository = new ProductRepository(clientFactory, productLogger);
        }

        /// <summary>
        /// Returns all orders from cache
        /// </summary>
        public static IEnumerable<Order> Orders
        {
            get 
            {
                return _orderRepository.Orders;
            }
        }

        /// <summary>
        /// Retuns all products from cache
        /// </summary>
        public static IEnumerable<Product> Products
        {
            get
            {
                return _productsRepository.Products;
            }
        }
    }
}
