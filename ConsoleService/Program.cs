using NServiceBus;
using NServiceBus.Logging;
using RestSharp;
using ServiceShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleService
{
    class Program
    {
        public static RestClient httpClient;
        static void Main(string[] args)
        {
            httpClient = new RestClient("http://api.icndb.com/jokes/random");
            Console.Title = "C# ConsoleService";
            BusConfiguration busConfiguration = new BusConfiguration();
            busConfiguration.EndpointName("ConsoleService.CS");
            busConfiguration.UseSerialization<JsonSerializer>();
            busConfiguration.EnableInstallers();
            busConfiguration.UsePersistence<InMemoryPersistence>();
            
            using (IBus bus = Bus.Create(busConfiguration).Start())
            {
                SendOrder(bus);
            }
        }
        static void SendOrder(IBus bus)
        {

            Console.WriteLine("Press enter to send a joke");
            Console.WriteLine("Press any key to exit");

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                Console.WriteLine();

                if (key.Key != ConsoleKey.Enter)
                {
                    return;
                }
                Guid id = Guid.NewGuid();

                SendJoke sendJoke = new SendJoke
                {
                    Message = GetJoke(),
                    Id = id
                };
                bus.Publish(sendJoke);

                Console.WriteLine("Sent a new joke message with id: {0}", id.ToString("N"));

            }

        }

        private static string GetJoke()
        {
            IRestRequest jokeRequest = new RestRequest();
            var resp = httpClient.Get(jokeRequest);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<JokeResponse>(resp.Content).Value.Joke;
        }
    }
}
