using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Threading.Tasks;

public class TokenValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHttpClientFactory _httpClientFactory;

    public TokenValidationMiddleware(RequestDelegate next, IHttpClientFactory httpClientFactory)
    {
        _next = next;
        _httpClientFactory = httpClientFactory;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/api/Auth"))
        {
            await _next(context);
            return;
        }

        var token = context.Request.Cookies["Authorization"];

        if (string.IsNullOrEmpty(token))
        {
            // Token is missing, redirect to login
            await RedirectToLogin(context);
            return;
        }

        if (!await IsTokenValid(token))
        {
            // Token is invalid, redirect to login
            await RedirectToLogin(context);
            return;
        }

        await _next(context);
    }

    private async Task<bool> IsTokenValid(string token)
    {
        var client = _httpClientFactory.CreateClient();
        try
        {
            var response = await client.GetAsync($"http://localhost:5003/api/Auth/validate?token={token}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    private async Task RedirectToLogin(HttpContext context)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetStringAsync("http://localhost:5003/api/Auth/login");
        context.Response.Cookies.Append("Authorization", response);
        context.Response.Redirect(context.Request.Path + context.Request.QueryString);
    }
}
