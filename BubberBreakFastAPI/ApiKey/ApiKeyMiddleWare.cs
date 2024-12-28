using Microsoft.AspNetCore.Mvc;

namespace BubberBreakFastAPI.ApiKey
{
    public class ApiKeyMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly string _apiKey;

        public ApiKeyMiddleWare(RequestDelegate next , IConfiguration configuration)
        {
            _next = next;
            _apiKey = configuration["ApiSettings:APIKey"];
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if(!httpContext.Request.Headers.TryGetValue("X-API-Key",out var extractedKey))
            {
                httpContext.Response.StatusCode = 401;
                await httpContext.Response.WriteAsync("Key is required");
                return;               
            }

            if (!string.Equals(extractedKey,_apiKey,StringComparison.OrdinalIgnoreCase))
            {
                httpContext.Response.StatusCode = 403;
                await httpContext.Response.WriteAsync("Key is not matched");
                return;
            }
           
           await _next(httpContext);
        }
    }
 }
    

