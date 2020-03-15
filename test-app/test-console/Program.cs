using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace test_console
{
    class Program
    {
        static readonly HttpClient _client = new HttpClient();

        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();

            var apiUrl = config["apiUrl"];

            if (string.IsNullOrEmpty(apiUrl))
            {
                throw new NullReferenceException($"apiUrl is not configured in appsetting.json");
            }

            var message = await _client.GetStringAsync(apiUrl);

            Console.WriteLine(message);
        }
    }
}
