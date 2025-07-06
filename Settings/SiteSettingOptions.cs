namespace PortfolioWebAPI.Settings;

public class SiteSettingOptions
{
    public static string SiteSettings = "SiteSettings";
    public bool ShowDetailedUnhandledExceptions { get; set; }
    public string DatabaseConnection { get; set; } = String.Empty;
    public string PortfolioApiKey { get; set; } = String.Empty;
    public string WebsiteIdentifierKey { get; set; } = String.Empty;
}