// See https://aka.ms/new-console-template for more information
using BFFApi.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace FrontendSimulator;

public class Program
{
    public static void Main(string[] args)
    {
        var app = Host
            .CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                var apiPort = Environment.GetEnvironmentVariable("API_PORT");
                var sleepDuration = Environment.GetEnvironmentVariable("SLEEP_DURATION");
                services.AddOptions<SpammingOptions>().Configure(options => options.SleepDuration = int.Parse(sleepDuration));
                services.AddHttpClient(typeof(ApiClient).FullName, x => x.BaseAddress = new Uri($"http://bffconsumptionapi:{apiPort}"));
                services.AddTransient<IApiClient>(serviceProvider =>
                {
                    var httpclientFactory = serviceProvider.GetService<IHttpClientFactory>();
                    return new ApiClient($"http://bffapi:{apiPort}", httpclientFactory.CreateClient(typeof(ApiClient).FullName));
                });
                services.AddHostedService<ApiSpammingService>();
            })
            .Build();

        app.Run();
    }
}