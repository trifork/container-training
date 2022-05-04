using BFFApi.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace FrontendSimulator
{
    public class ApiSpammingService : IHostedService
    {
        private readonly IApiClient _client;
        private readonly SpammingOptions _options;
        private readonly Random _random = new Random();
        private Timer timer;


        public ApiSpammingService(IApiClient client, IOptions<SpammingOptions> options)
        {
            _client = client;
            _options = options.Value;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(_ => MakeRequest(), null, 0, _options.SleepDuration);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            if(timer != null)
            {
                timer.Dispose();
            }

            return Task.CompletedTask;
        }

        public void MakeRequest()
        {
            var now = DateTime.Now;
            _client.GetAnalyticsForPeriodAsync(now.AddDays(-_random.Next(1, 30)), now);
        }
    }
}
