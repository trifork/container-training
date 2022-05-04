using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BFFConsumptionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumptionController : ControllerBase
    {
        [HttpGet(Name ="GetConsumption")]
        public async Task<Consumption> GetConsumption(DateTimeOffset fromDate, DateTimeOffset toDate) 
        {
            var days = (toDate - fromDate).Days;
            return new Consumption()
            {
                Value = days * 2.5,
                Unit = ConsumptionType.Kwh
            };
        }
    }
}
