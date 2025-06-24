namespace PortfolioWebAPI;

public static class StringExtensionMethods
{
    public static bool ToBool(this string? val)
    {
        return Boolean.TryParse(val, out _);
    }

    public static bool IsEmpty(this string? val)
    {
        return String.IsNullOrEmpty(val);
    }

    public static bool Exists(this string? val)
    {
        return !val.IsEmpty();
    }
}