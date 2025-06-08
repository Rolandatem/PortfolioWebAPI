using System.Net;
using Microsoft.Extensions.Options;
using PortfolioWebAPI.Settings;

namespace PortfolioWebAPI.Middleware;

internal class UnhandledExceptionMiddleware(
    RequestDelegate _next,
    ILogger<UnhandledExceptionMiddleware> _logger,
    IOptions<SiteSettingOptions> _siteSettings)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled Exception");
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    public Task HandleExceptionAsync(
        HttpContext httpContext,
        Exception ex)
    {
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new
        {
            message = "An unhandled exception occured",
            exceptionMessage = _siteSettings.Value.ShowDetailedUnhandledExceptions
                ? ex.Message
                : String.Empty,
            stackTrace = _siteSettings.Value.ShowDetailedUnhandledExceptions
                ? ex.StackTrace
                : String.Empty
        };

        return httpContext.Response.WriteAsJsonAsync(response);
    }
}