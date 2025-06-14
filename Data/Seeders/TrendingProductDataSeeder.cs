using PortfolioWebAPI.Data.Models;
using PortfolioWebAPI.Enums;
using PortfolioWebAPI.Interfaces;

namespace PortfolioWebAPI.Data.Seeders;

internal class TrendingProductDataSeeder(
    PortfolioDbContext _context) : IDataSeeder
{
    public async Task SeedAsync()
    {
        await _context.TrendingProducts.AddRangeAsync([
            new TrendingProduct()
            {
                ProductName = "Next-Day Rubber Rolls",
                SKU = "ROLL-1000",
                ImageUrl = "trending/next-day-rubber-rolls.webp",
                Stars = 5,
                Reviews = 4,
                ColorCount = 4,
                Description = "Orders before 12 pm Eastern will ship by the same or next business day.",
                SalePrice = 1.19M,
                OriginalPrice = 1.49M,
                SavingsPercentage = 20,
                ShipType = (int)ShipType.NextDay
            },
            new TrendingProduct()
            {
                ProductName = "VersaStep Pro Marley Dance Floor Rolls",
                SKU = "DANCE-1000",
                ImageUrl = "trending/versa-step-pro-marley-dance-floor-rolls.webp",
                Stars = 4,
                Reviews = 26,
                ColorCount = 2,
                Description = "Versatile & durable marley floor, great for all dance styles.",
                SalePrice = 2.28M,
                OriginalPrice = 3.19M,
                SavingsPercentage = 29,
                ShipType = (int)ShipType.QuickShip
            },
            new TrendingProduct()
            {
                ProductName = "3/8\" Heavy Duty Rubber Rolls",
                SKU = "ROLL-1001",
                ImageUrl = "trending/3-8-heavy-duty-rubber-rolls.webp",
                Stars = 4,
                Reviews = 351,
                ColorCount = 38,
                Description = "Highest quality rubber gym flooring available.",
                SalePrice = 1.58M,
                OriginalPrice = 2.26M,
                SavingsPercentage = 30,
                ShipType = (int)ShipType.QuickShip
            },
            new TrendingProduct()
            {
                ProductName = "Ribbed Carpet Tiles - Quick Ship",
                SKU = "CARPET-1000",
                ImageUrl = "trending/rubbed-carpet-tiles-quick-ship.webp",
                Stars = 4,
                Reviews = 245,
                ColorCount = 13,
                Description = "18\" x 18\" Tile - 2.25 sq ft peel and stick installation.",
                SalePrice = .99M,
                OriginalPrice = 1.13M,
                SavingsPercentage = 12,
                ShipType = (int)ShipType.FreeNextDay
            },
            new TrendingProduct()
            {
                ProductName = "TritonCORE Pro 7\" Rigid Core Vinyl Planks",
                SKU = "VINYL-1000",
                ImageUrl = "trending/tritoncore-pro-7-rigid-core-vinyl-planks.webp",
                Stars = 4,
                Reviews = 34,
                ColorCount = 10,
                Description = "Easy DIY installation - can be installed on any level.",
                SalePrice = 1.59M,
                OriginalPrice = 1.99M,
                SavingsPercentage = 20,
                ShipType = (int)ShipType.QuickShip
            },
            new TrendingProduct()
            {
                ProductName = "Nitro Garage Floor Tiles",
                SKU = "GARAGE-1000",
                ImageUrl = "trending/nitro-garage-floor-tiles.webp",
                Stars = 4,
                Reviews = 395,
                ColorCount = 8,
                Description = "DIY friendly, easy snap-together installation.",
                SalePrice = 1.99M,
                OriginalPrice = 3.35M,
                SavingsPercentage = 41,
                ShipType = (int)ShipType.FreeQuickShip
            },
            new TrendingProduct()
            {
                ProductName = "Jamboree Rubber Playground Tiles",
                SKU = "TILE-1000",
                ImageUrl = "trending/jamboree-rubber-playground-tiles.webp",
                Stars = 4,
                Reviews = 21,
                ColorCount = 12,
                Description = "Sustainability made in the USA, 15-year limited warranty.",
                SalePrice = 6.75M,
                OriginalPrice = 10.12M,
                SavingsPercentage = 33,
                ShipType = (int)ShipType.QuickShip
            },
            new TrendingProduct()
            {
                ProductName = "Helios Composite Deck Tiles",
                SKU = "DECK-1000",
                ImageUrl = "trending/helios-composite-deck-tiles.webp",
                Stars = 5,
                Reviews = 30,
                ColorCount = 3,
                Description = "Eco-friendly and durable composite decking.",
                SalePrice = 4.75M,
                OriginalPrice = 6.78M,
                SavingsPercentage = 30,
                ShipType = (int)ShipType.FreeNextDay
            },
            new TrendingProduct()
            {
                ProductName = "Indoor Basketball Court Tiles",
                SKU = "TILE-1001",
                ImageUrl = "trending/indoor-basketball-court-tiles.webp",
                Stars = 4,
                Reviews = 9,
                ColorCount = 6,
                Description = "7-year warranty. Easy to install and maintain.",
                SalePrice = 2.19M,
                OriginalPrice = 3.25M,
                SavingsPercentage = 33,
                ShipType = (int)ShipType.FreeShipping
            },
            new TrendingProduct()
            {
                ProductName = "Performance Gym Turf Rolls",
                SKU = "ROLL-1003",
                ImageUrl = "trending/performance-gym-turf-rolls.webp",
                Stars = 4,
                Reviews = 19,
                ColorCount = 2,
                Description = "Max cut length 82'. Indoor/outdoor turf.",
                SalePrice = 2.49M,
                OriginalPrice = 3.99M,
                SavingsPercentage = 38,
                ShipType = (int)ShipType.NextDay
            }
        ]);

        await _context.SaveChangesAsync();
    }
}