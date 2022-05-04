using BFFBillingApi.Client;
using BFFConsumptionApi.Client;
using Microsoft.AspNetCore.Mvc;

namespace BFFApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private IConsumptionClient _consumptionClient;
        private IBillingApiClient _billingApiClient;
        private ILogger _logger;

        public AnalyticsController(IConsumptionClient consumptionClient, IBillingApiClient billingApiClient, ILogger<AnalyticsController>  logger)
        {
            _consumptionClient = consumptionClient;
            _billingApiClient = billingApiClient;
            _logger = logger;
        }

        [HttpGet(Name ="GetAnalyticsForPeriod")]
        public async Task<AnalyticsDTO> GetAnalyticsForPeriod(DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            _logger.LogInformation("Enter {methodName}", nameof(GetAnalyticsForPeriod));
            Consumption? consumption = null;
            int? billingInfo = null;
            try
            {
                billingInfo = await _billingApiClient.GetPriceAsync(fromDate, toDate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                Console.WriteLine(ex.InnerException.GetType().FullName);
            }
            try
            {
                consumption = await _consumptionClient.GetConsumptionAsync(fromDate, toDate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                Console.WriteLine(ex.InnerException.GetType().FullName);
            }

            return new AnalyticsDTO { Consumption = consumption, Price = billingInfo };
        }
    }
}
