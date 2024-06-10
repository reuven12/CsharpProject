using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpGet("validate")]
    public IActionResult ValidateToken(string token)
    {
        return Ok("Token is valid");
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        var newToken = "new_token_example"; 
        return Ok(newToken);
    }
}
