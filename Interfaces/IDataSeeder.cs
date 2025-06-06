namespace PortfolioWebAPI.Interfaces;

/// <summary>
/// Interface definition for a class that will seed data into
/// the database.
/// </summary>
public interface IDataSeeder
{
    /// <summary>
    /// Seeds specific data to the database.
    /// </summary>
    /// <returns></returns>
    Task SeedAsync();
}