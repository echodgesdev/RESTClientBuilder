using System;
using System.Collections.Generic;
using System.Linq;
using Traffic.RestClients.DummyAPI;

namespace DummyConsumer
{
    public class APIFacade
    {
        private string _baseUrl;

        public APIFacade(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public List<City> GetCities()
        {
            using (var c = new System.Net.Http.HttpClient())
            {
                var testClient = new Client(_baseUrl, c);

                return testClient.CityAsync().Result.ToList();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var cities = new APIFacade("https://localhost:44367/").GetCities();

            foreach (var city in cities)
            {
                Console.WriteLine($"{city.State}, {city.Name}, {city.Population}");
            }
            Console.Write("Done.");
            Console.ReadLine();
        }
    }
}
