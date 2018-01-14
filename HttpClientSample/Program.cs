using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HttpClientSample
{
    public class Product
    {
        //public int id;

        public string copyright { get; set; }
        public List<Team> teams { get; set; }

        public Product() { }
    }

    public class Team
    {
        public int id { get; set; }
        public string name { get; set; }
        public string link { get; set; }
        // public Venue ven {get; set;}
        public string abbreviation { get; set; }
        public string teamName { get; set; }
        public string locationName { get; set; }
        public string firstYearOfPlay { get; set; }
        // public Division divis {get; set;}
        // public Conference conf {get;set;}
        // public Franchise fran {get;set;}
        public string shortName { get; set; }
        public string officialSiteUrl { get; set; }
        public int franchiseId { get; set; }
        public bool active { get; set; }

        public Team() { }

    }
    
    class HockeyAPI
    {
        HttpClient client;

        public HockeyAPI()
        {
            client = new HttpClient();

            client.BaseAddress = new Uri("https://statsapi.web.nhl.com/api/v1/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Product> GetTeams()
        {
            Product product = null;

            try
            {

                HttpResponseMessage response = await client.GetAsync("teams");
                if (response.IsSuccessStatusCode)
                {
                    product = await response.Content.ReadAsAsync<Product>();
                

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return product;
        }

    }

    class Program
    {
        static HttpClient client = new HttpClient();

        static void Main()
        {
 
            HockeyAPI hapi = new HockeyAPI();
            Product p;
            p = hapi.GetTeams().GetAwaiter().GetResult();
            Console.WriteLine("Copyright notice: " + p.copyright);
            for (int i = 0; i < p.teams.Count; i++)
            {
                Console.WriteLine("ID: " + p.teams[i].id + "\t" + "Team Name: " + p.teams[i].name);
            }
            Console.ReadLine();
        }

       
    }
}
