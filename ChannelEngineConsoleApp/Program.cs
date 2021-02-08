using ChannelEngineApp;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChannelEngineConsoleApp
{
    class Program
    {
          ///////////////////////////////////////////////////
         // Deprecated App: Please use Postman or Newman. //
        ///////////////////////////////////////////////////

        static string ApiKey = "541b989ef78ccb1bad630ea5b85c6ebff9ca3322";
        static string MerchantHost = "https://api-dev.channelengine.net/api/v2/";
        static bool Success;

        static async Task GetOrdersTop5SortedAscending()
        {
            HttpClient client = new HttpClient();
            string requestUri = MerchantHost + "orders?statuses=IN_PROGRESS&page=1&apikey=" + ApiKey;
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            request.Headers.Add("Accept", "application/json");
            var response = await client.GetAsync(requestUri);

            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();

                try
                {
                    var orderSet = JsonSerializer.Deserialize<OrderSet>(responseString);
                //  Console.WriteLine(Formatter.Format<Order>(orderSet.Content));
                    Console.WriteLine(JsonSerializer.Serialize(orderSet));
                    Success = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR:"+e.Message);
                    Success = false;
                }
            }
            else
            {
                Console.WriteLine("SERVER RESPONDED WITH STATUS CODE "+ response.StatusCode);
                Success = false;
            }
        }

        static async Task GetProducts()
        {
            HttpClient client = new HttpClient();
            string requestUri = MerchantHost + "products?search=001201&apikey=" + ApiKey;
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            request.Headers.Add("Accept", "application/json");
            var response = await client.GetAsync(requestUri);

            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();

                try
                {
                    var productSet = JsonSerializer.Deserialize<ProductSet>(responseString);
                //  Console.WriteLine(Formatter.Format<Product>(productSet.Content));
                    Console.WriteLine(JsonSerializer.Serialize(productSet));
                    Success = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR:" + e.Message);
                    Success = false;
                }
            }
            else
            {
                Console.WriteLine("SERVER RESPONDED WITH STATUS CODE " + response.StatusCode);
                Success = false;
            }
        }

        static async Task SetProductStock(string merchantProductNo, int value)
        {
            string payload = JsonSerializer.Serialize<ProductStock>(new ProductStock { Value = value });
            HttpClient client = new HttpClient();
            string requestUri = MerchantHost + "products/" + merchantProductNo + "?apikey=" + ApiKey;
            var request = new HttpRequestMessage(HttpMethod.Patch, requestUri);
            request.Headers.Add("Accept", "application/json");

            request.Content = new StringContent("[" + payload + "]", Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);

            Success = response.IsSuccessStatusCode;
        }

        static void  Main()
        {
            bool run = true;

            Console.WriteLine("Menu");
            Console.WriteLine("----");
            Console.WriteLine("O: Get orders top 5 ordered");
            Console.WriteLine("P: Get products");
            Console.WriteLine("0-4: Adjust Stock to 25 ( 1:S , 2:M, 3:L , 4:XL )");
            Console.WriteLine("X: EXIT");

            while (run)
            {
                string cmd = Console.ReadKey().KeyChar.ToString();

                switch(cmd)
                {
                    case "O":
                    case "o":
                        Console.WriteLine(" -> Retrieving top 5 orders...");
                        GetOrdersTop5SortedAscending().GetAwaiter().GetResult();
                        Console.WriteLine("[done]");
                        break;
                    case "P":
                    case "p":
                        Console.WriteLine(" -> Retrieving products...");
                        GetProducts().GetAwaiter().GetResult();
                        Console.WriteLine("[done]");
                        break;
                    case "0":
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                        string[] merchantProductNoArray = new string[] { "001201", "001201-S", "001201-M", "001201-L", "001201-XL" };
                        int index = int.Parse(cmd);
                        SetProductStock(merchantProductNoArray[index],25).GetAwaiter().GetResult();
                        if (Success)
                        {
                            Console.WriteLine(" -> Product stock adjusted to 25 for " + merchantProductNoArray[index]);
                        }
                        else
                        {
                            Console.WriteLine(" -> Failed to adjust stock for " + merchantProductNoArray[index]);
                        }
                        break;
                    case "X":
                    case "x":
                        run = false;
                        break;
                    default:
                        Console.WriteLine(" (No operation)");
                      break;
                }
            }

        }
    }
}
