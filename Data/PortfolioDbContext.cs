using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PortfolioWebAPI.Data.Models;

namespace PortfolioWebAPI.Data;

/// <summary>
/// DbContext for the Portfolio DbContext.
/// </summary>
public class PortfolioDbContext : DbContext
{
    #region "Constructor"
    public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options)
        : base(options)
    {

    }
    #endregion

    #region "Tables"
    public DbSet<TrendingProduct> TrendingProducts { get; set; } = null!;
    #endregion

    #region "Configuring"
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder
            .UseInMemoryDatabase("Portfolio");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        Assembly assembly = GetType().Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
    }
    #endregion

    #region "Seeding"
    public async Task SeedDataAsync()
    {
        if (Database.EnsureCreated())
        {
            await SaveChangesAsync();
        }
    }
    #endregion
}