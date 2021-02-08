using MerchantData;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MerchantLogic
{
    public class OrderRepository : IBaseOrderRepository
    {
        readonly IHttpClientFactory _clientFactory;
        readonly ILogger<OrderRepository> _logger;

        public OrderRepository(IHttpClientFactory clientFactory, ILogger<OrderRepository> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        /// <summary>
        /// Orders retrieved from the dataset
        /// </summar
        public IEnumerable<Order> Orders { get; private set; }

        /// <summary>
        /// Retrieves the latest version of the orders dataset and stores it in the Orders property
        /// </summary>
        /// <returns></returns>
        public async Task Sync()
        {
            try
            {
                string requestUri = RepositoryConfig.Host + "orders?statuses=IN_PROGRESS&page=1&apikey=" + RepositoryConfig.ApiKey;
                var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
                request.Headers.Add("Accept", "application/json");

                var client = _clientFactory.CreateClient();

                var response = await client.GetAsync(requestUri);

                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();

                    var orderSet = JsonSerializer.Deserialize<OrderSet>(responseString);
                    Orders = orderSet.Content;
                }
                else
                {
                    Orders = Array.Empty<Order>();
                    _logger.LogError(response.StatusCode + " status code was returned");
                }
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
    }
}
