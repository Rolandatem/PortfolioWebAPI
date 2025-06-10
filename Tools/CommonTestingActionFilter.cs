using Microsoft.AspNetCore.Mvc.Filters;

namespace PortfolioWebAPI.Tools;

/// <summary>
/// Action filter used to set up the 'withDelay' and
/// 'withError' optional query parameters for use in
/// controllers without having to code for them
/// in every single endpoint.
/// </summary>
internal class CommonTestingActionFilter : IActionFilter
{
    #region "IActionFilter"
    public void OnActionExecuting(ActionExecutingContext context)
    {
        bool withDelay = context
            .HttpContext
            .Request
            .Query["withDelay"]
            .FirstOrDefault()
            .ToBool();

        bool withError = context
            .HttpContext
            .Request
            .Query["withError"]
            .FirstOrDefault()
            .ToBool();

        //--Store for controller testing.
        context.HttpContext.Items["withDelay"] = withDelay;
        context.HttpContext.Items["withError"] = withError;
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
    #endregion
}