using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PortfolioWebAPI.Data.Models;
using PortfolioWebAPI.Settings;

namespace PortfolioWebAPI.Data;

/// <summary>
/// DbContext for the Portfolio DbContext.
/// </summary>
public class PortfolioDbContext : DbContext
{
    #region "Member Variables"
    ILogger<PortfolioDbContext>? _logger = null;
    readonly SiteSettingOptions? _siteSettings = null;
    #endregion

    #region "Constructor"
    public PortfolioDbContext(
        DbContextOptions<PortfolioDbContext> options,
        ILogger<PortfolioDbContext> logger,
        IOptions<SiteSettingOptions> settings)
        : base(options)
    {
        _siteSettings = settings.Value;
        _logger = logger;
    }
    #endregion

    #region "Tables"
    public DbSet<TrendingProduct> TrendingProducts { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    #endregion

    #region "Configuring"
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        _logger?.LogInformation("DB SERVER: {server}", _siteSettings?.PortfolioDbServer);

        // optionsBuilder
        //     .UseInMemoryDatabase("Portfolio");
        optionsBuilder
            .UseSqlServer($"Server={_siteSettings?.PortfolioDbServer},1433;Database=PortfolioDB;User Id=sa;Password=SomePassword#1;TrustServerCertificate=True");

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        Assembly assembly = GetType().Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
    }
    #endregion
}