using System.Net;
using Microsoft.Extensions.Options;
using PortfolioWebAPI.Settings;

namespace PortfolioWebAPI.Middleware;

/// <summary>
/// Middleware used to set up an error response for
/// unhandled exceptions.
/// </summary>
/// <param name="_next">Next item in pipeline.</param>
/// <param name="_siteSettings">Global site settings.</param>
internal class UnhandledExceptionMiddleware(
    RequestDelegate _next,
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
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    public Task HandleExceptionAsync(
        HttpContext httpContext,
        Exception ex)
    {
        //--Dont interfere if a response already started.
        if (httpContext.Response.HasStarted)
        {
            return Task.CompletedTask;
        }

        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new
        {
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