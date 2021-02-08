using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChannelEngineApp
{
    public class ProductRepository : IBaseProductRepository
    {
        readonly IHttpClientFactory _clientFactory;
        readonly ILogger<ProductRepository> _logger;

        public ProductRepository(IHttpClientFactory clientFactory, ILogger<ProductRepository> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        /// <summary>
        /// Products retrieved from the dataset
        /// </summary>
        public IEnumerable<Product> Products { get; private set; }

        /// <summary>
        /// Retrieves the latest version of the products dataset and stores it in the Products property
        /// </summary>
        /// <returns></returns>
        public async Task Sync()
        {
            try
            {
                var client = _clientFactory.CreateClient();
                string requestUri = RepositoryConfig.Host + "products?search=001201&apikey=" + RepositoryConfig.ApiKey;
                var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
                request.Headers.Add("Accept", "application/json");
                var response = await client.GetAsync(requestUri);

                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();

                    var productSet = JsonSerializer.Deserialize<ProductSet>(responseString);
                    Products = productSet.Content;
                }
                else
                {
                    _logger.LogError(response.StatusCode + " status code was returned");
                    Products = Array.Empty<Product>();
                }

            }
            catch (Exception e) 
            {
                _logger.LogError(e.Message);
            }
        }

        /// <summary>
        /// Changes a single value within the merchant data set
        /// </summary>
        /// <param name="key">The merchant product number</param>
        /// <param name="property">The property name to change. (Currently only 'Stock' is supported)</param>
        /// <param name="value">The value to set</param>
        /// <returns></ret
        public async Task Set(string key, string property, int value)
        {
            try
            {
                string payload;

                switch (property)
                {
                    case "Stock":
                        payload = JsonSerializer.Serialize<ProductStock>(new ProductStock { Value = value });
                        break;
                    default:
                        return;
                }

                var client = _clientFactory.CreateClient();
                string requestUri = RepositoryConfig.Host + "products/" + key + "?apikey=" + RepositoryConfig.ApiKey;
                var request = new HttpRequestMessage(HttpMethod.Patch, requestUri);
                    request.Headers.Add("Accept", "application/json");

                request.Content = new StringContent("["+payload+"]", Encoding.UTF8, "application/json");

                var response = await client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError(response.StatusCode+" status code was returned");
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
    }
}
