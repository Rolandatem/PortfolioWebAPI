using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using PortfolioWebAPI.Settings;

namespace PortfolioWebAPI.Middleware;

public class ApiKeyMiddleware(
    RequestDelegate _next,
    IOptions<SiteSettingOptions> _siteSettings)
{
    #region "Private Properties"
    /// <summary>
    /// Specifying which routes need to be secured, which with the current
    /// setup is less than the allowed routes. This is pretty much to allow
    /// the use of /scalar openapi in dev and docker compose.
    /// </summary>
    readonly List<string> SecuredStarterSegments = new List<string>()
    {
        "/api"
    };
    #endregion

    public async Task InvokeAsync(HttpContext context)
    {
        //--Only protect specific routes.
        if (SecuredStarterSegments.Any(segment =>
            context.Request.Path.StartsWithSegments(segment, StringComparison.OrdinalIgnoreCase)))
        {
            //--Require x-site-api-key header for secured endpoints.
            if (context.Request.Headers.TryGetValue("x-site-api-key", out StringValues extractedApiKey) == false ||
                extractedApiKey != _siteSettings.Value.PortfolioApiKey)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Forbidden");
                return;
            }
        }

        await _next(context);
    }
}