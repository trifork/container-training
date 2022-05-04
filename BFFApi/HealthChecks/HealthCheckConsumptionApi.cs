using BFFConsumptionApi.Client;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BFFApi
{
    public class HealthCheckConsumptionApi : IHealthCheck
    {
        private readonly HttpClient _httpClient;
        public HealthCheckConsumptionApi(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(typeof(ConsumptionClient).FullName);
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
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
            }catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("Can't reach Consumption API");
            }
            
        }
    }
}
