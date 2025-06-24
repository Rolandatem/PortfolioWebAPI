namespace PortfolioWebAPI.Settings;

public class SiteSettingOptions
{
    public static string SiteSettings = "SiteSettings";
    public bool ShowDetailedUnhandledExceptions { get; set; }
    public string PortfolioDbServer { get; set; } = String.Empty;
    public string[] AllowedOrigins { get; set; } = [];
}