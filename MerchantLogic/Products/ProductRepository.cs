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
        IHttpClientFactory _clientFactory;

        public ProductRepository(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public IEnumerable<Product> Products { get; private set; }

        public int PageIndex = 1;
        public bool? Succes = null;

        public async Task Sync()
        {
            Succes = null;

            var client = _clientFactory.CreateClient();
            string requestUri = RepositoryConfig.Host + "products?search=001201&apikey=" + RepositoryConfig.ApiKey;
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
                request.Headers.Add("Accept", "application/json");
            var response = await client.GetAsync(requestUri);

            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();

                try
                {
                    var productSet = JsonSerializer.Deserialize<ProductSet>(responseString);
                    Products = productSet.Content;
                    Succes = true;
                }
                catch(Exception)
                {
                    Products = Array.Empty<Product>();
                    Succes = false;
                }
            }
            else
            {
                Products = Array.Empty<Product>();
                Succes = false;
            }
        }

        public async Task Set(string key, string property, int value)
        {
            Succes = null;

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
                // TODO
            }
        }
    }
}
