using System.Collections.Generic;
using System.Net.Http;

namespace ChannelEngineApp
{
    public class Repository
    {
        private static OrderRepository _orderRepository;
        private static ProductRepository _productsRepository;

        public Repository(IHttpClientFactory clientFactory)
        {
            _orderRepository = new OrderRepository(clientFactory);
            _productsRepository = new ProductRepository(clientFactory);
        }

        public static IEnumerable<Order> Orders
        {
            get 
            {
                return _orderRepository.Orders;
            }
        }

        public static IEnumerable<Product> Products
        {
            get
            {
                return _productsRepository.Products;
            }
        }
    }
}
