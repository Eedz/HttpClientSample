using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HttpClientSample
{
    public class Product
    {
        public int id;
        public string name;

     
    }

    class Program
    {
        static HttpClient client = new HttpClient();

        static void Main()
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
 
            client.BaseAddress = new Uri("https://statsapi.web.nhl.com/api/v1/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                Product product;

                
                HttpResponseMessage response = await client.GetAsync("teams/15");
                if (response.IsSuccessStatusCode)
                {
                    product = await response.Content.ReadAsAsync<Product>();
                    Console.WriteLine(product.id);
                    Console.WriteLine(product.name);
                }

                

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
