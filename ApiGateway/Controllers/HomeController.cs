using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    public HomeController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet("microservice1")]
    public async Task<IActionResult> GetFromMicroservice1()
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetStringAsync("http://mymicroservice1:80/api/My");
        return Ok(response);
    }

    [HttpGet("microservice2")]
    public async Task<IActionResult> GetFromMicroservice2()
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetStringAsync("http://mymicroservice2:80/api/My");
        return Ok(response);
    }

}
