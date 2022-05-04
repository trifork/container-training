using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BFFBillingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        [HttpGet(Name = "GetPrice")]
        public async Task<int> Get(DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            var days = (toDate - fromDate).Days;
            return days * 15;
        }
    }
}
