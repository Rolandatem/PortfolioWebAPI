namespace PortfolioWebAPI;

public static class StringExtensionMethods
{
    public static bool ToBool(this string? val)
    {
        return Boolean.TryParse(val, out _);
    }
}