using PortfolioWebAPI.Interfaces;

namespace PortfolioWebAPI.Services;

/// <summary>
/// Service used to seed the database with initial data. Since
/// this portfolio web api uses an in-memory database we can't
/// use traditional migrations to seed.
/// </summary>
/// <param name="_serviceProvider">IServiceProvider</param>
public class SeedingService(
    IServiceProvider _serviceProvider
)
{
    /// <summary>
    /// Main seeder method that calls all individual seeders.
    /// </summary>
    /// <returns></returns>
    public async Task SeedDatabaseAsync()
    {
        IEnumerable<IDataSeeder> seeders = _serviceProvider.GetServices<IDataSeeder>();

        List<Task> seedingTasks = new List<Task>();
        foreach (IDataSeeder seeder in seeders)
        {
            seedingTasks.Add(seeder.SeedAsync());
        }

        await Task.WhenAll(seedingTasks);
    }
}