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
    internal DbSet<TrendingProduct> TrendingProducts { get; set; } = null!;
    internal DbSet<Category> Categories { get; set; } = null!;
    internal DbSet<FAQ> FAQs { get; set; } = null!;
    internal DbSet<Product> Products { get; set; } = null!;
    internal DbSet<TagType> TagTypes { get; set; } = null!;
    internal DbSet<Tag> Tags { get; set; } = null!;
    internal DbSet<SiteFilterTagType> SiteFilterTagTypes { get; set; } = null!;
    internal DbSet<ProductTag> ProductTags { get; set; } = null!;
    internal DbSet<ImageType> ImageTypes { get; set; } = null!;
    internal DbSet<ProductImage> ProductImages { get; set; } = null!;
    internal DbSet<ShoppingCart> ShoppingCarts { get; set; } = null!;
    internal DbSet<ShoppingCartLineItem> ShoppingCartLineItems { get; set; } = null!;
    internal DbSet<OrderDetail> OrderDetails { get; set; } = null!;
    #endregion

    #region "Configuring"
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        _logger?.LogInformation("DB SERVER: {server}", _siteSettings?.PortfolioDbServer);

        // optionsBuilder
        //     .UseInMemoryDatabase("Portfolio");
        // optionsBuilder
        //     .UseSqlServer($"Server={_siteSettings?.PortfolioDbServer},1433;Database=PortfolioDB;User Id=sa;Password=SomePassword#1;TrustServerCertificate=True");
        optionsBuilder
            .UseNpgsql($"Host={_siteSettings?.PortfolioDbServer};Port=5432;Database=portfoliodb;Username=sa;Password=SomePassword#1", options =>
            {
                //--This helps in performance when an object with multiple
                //--navigation properties are requested.
                options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        Assembly assembly = GetType().Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
    }
    #endregion
}