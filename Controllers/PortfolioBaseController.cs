using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;

namespace PortfolioWebAPI.Controllers;

/// <summary>
/// Controller base class for common PortfolioAPI functions and properties.
/// </summary>
public abstract class PortfolioBaseController : ControllerBase
{
    /// <summary>
    /// Indicates request wants to use the 2 second delay test.
    /// </summary>
    protected bool WithDelay => Convert.ToBoolean(HttpContext.Items["withDelay"]);

    /// <summary>
    /// Indicates request wants to use the error test.
    /// </summary>
    protected bool WithError => Convert.ToBoolean(HttpContext.Items["withError"]);

    /// <summary>
    /// Quick tool to do controller tests if requested from API calls.
    /// </summary>
    /// <param name="callerName">Name of the method calling the test.</param>
    /// <exception cref="Exception">Test exception if requested.</exception>
    protected async Task DoTestsAsync(
        [CallerMemberName] string callerName = null!)
    {
        if (this.WithDelay) { await Task.Delay(2000); }
        if (this.WithError)
        {
            throw new Exception($"Test Exception from {callerName}");
        }
    }
}