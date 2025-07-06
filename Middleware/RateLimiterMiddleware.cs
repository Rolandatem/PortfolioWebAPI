using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using PortfolioWebAPI.Settings;

namespace PortfolioWebAPI.Middleware;

public class RateLimiterMiddleware(
    RequestDelegate _next,
    IMemoryCache _memoryCache,
    IOptions<SiteSettingOptions> _siteSettings)
{
    #region "Member Variables"
    readonly int _limitCount = 30; //--Max requests.
    readonly TimeSpan _limitWindow = TimeSpan.FromMinutes(1);
    #endregion

    public async Task InvokeAsync(HttpContext context)
    {
        //--Bypass for trusted internal website calls.
        if (context.Request.Headers.TryGetValue("x-internal-website-key", out StringValues websiteKey) &&
            websiteKey == _siteSettings.Value.WebsiteIdentifierKey)
        {
            await _next(context);        
        }

        string clientIP = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        //--Include API Key so we can identify that the abuser knows the
        //--key.
        if (websiteKey.ToString().Exists())
        {
            clientIP = $"{websiteKey}_{clientIP}";
        }

        //--Setup check
        string cacheKey = $"rate_limit:{clientIP}";
        DateTimeOffset now = DateTimeOffset.UtcNow;

        //--Get limit window data.
        var entry = _memoryCache.Get<(int count, DateTimeOffset windowStart)?>(cacheKey);
        if (entry == null || now - entry.Value.windowStart > _limitWindow)
        {
            //--Start new window.
            entry = (1, now);
        }
        else
        {
            //--Use existing
            entry = (entry.Value.count + 1, entry.Value.windowStart);
        }

        //--Check for violation
        if (entry.Value.count > _limitCount)
        {
            //--Too Many Requests code.
            context.Response.StatusCode = 429;
            //--Set retry-after header per RFC 6585
            context.Response.Headers.RetryAfter = _limitWindow.TotalSeconds.ToString();
            await context.Response.WriteAsync("Too many requests - rate limit exceeded.");
            return;
        }

        //--Set cache entry with absolute expiration at window end.
        _memoryCache.Set(cacheKey, entry, entry.Value.windowStart.Add(_limitWindow));

        await _next(context);
    }
}