using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChannelEngineApp
{
    public class OrderRepository : IBaseOrderRepository
    {
        readonly IHttpClientFactory _clientFactory;

        public OrderRepository(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public IEnumerable<Order> Orders { get; private set; }

        public int PageIndex = 1;
        public bool? Succes = null;

        public async Task Sync()
        {
            Succes = null;

            string requestUri = RepositoryConfig.Host + "orders?statuses=IN_PROGRESS&page=1&apikey=" + RepositoryConfig.ApiKey;
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
                request.Headers.Add("Accept", "application/json");


            var client = _clientFactory.CreateClient();

            var response = await client.GetAsync(requestUri);

            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();

                try
                {
                    var orderSet = JsonSerializer.Deserialize<OrderSet>(responseString);
                    Orders = orderSet.Content;
                    Succes = true;
                }
                catch(Exception)
                {
                    Orders = Array.Empty<Order>();
                    Succes = false;
                }
            }
            else
            {
                Orders = Array.Empty<Order>();
                Succes = false;
            }
        }
    }
}
