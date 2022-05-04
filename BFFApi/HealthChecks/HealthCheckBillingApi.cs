using BFFBillingApi.Client;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BFFApi.HealthChecks
{
    public class HealthCheckBillingApi : IHealthCheck
    {
        private readonly HttpClient _httpClient;
        public HealthCheckBillingApi(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(typeof(BillingApiClient).FullName);
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var res = await _httpClient.GetAsync("health");
            if (res.IsSuccessStatusCode)
            {
                return HealthCheckResult.Healthy("Consumption API is healthy");
            }
            else
            {
                return HealthCheckResult.Unhealthy("Consumption API is unhealthy");
            }
        }
    }
}
