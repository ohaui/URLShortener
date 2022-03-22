using Microsoft.AspNetCore.Http.Extensions;
using URLShortener.Data;

namespace URLShortener.Middleware;

public class RedirectMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RedirectMiddleware> _logger;

    public RedirectMiddleware(RequestDelegate next, ILogger<RedirectMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext, ShortenerContext context)
    {
        var query = httpContext.Request.GetDisplayUrl();
        var link = context.Links.FirstOrDefault(x => x.Shortened == query);
        
        if (link != default)
        {
            httpContext.Response.Redirect(link.Original);
        }
        else
        {
            await _next.Invoke(httpContext);
        }
    }
}