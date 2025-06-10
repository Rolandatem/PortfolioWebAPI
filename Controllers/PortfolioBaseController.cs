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
}