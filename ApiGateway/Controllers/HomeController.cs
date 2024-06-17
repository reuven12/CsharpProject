using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetStringAsync("http://usersservice/api/users");
            return Ok(response);
        }

        [HttpGet("orders")]
        public async Task<IActionResult> GetOrders()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetStringAsync("http://ordersservice/api/orders");
            return Ok(response);
        }

        // Add more methods as needed for other CRUD operations
    }
}
