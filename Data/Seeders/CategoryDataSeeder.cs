using PortfolioWebAPI.Data.Models;
using PortfolioWebAPI.Interfaces;

namespace PortfolioWebAPI.Data.Seeders;

internal class CategoryDataSeeder(
    PortfolioDbContext _context) : IDataSeeder
{
    public async Task SeedAsync()
    {
        await _context.Categories.AddRangeAsync([
            new Category() {
                Id = 1,
                Name = "Rubber Flooring",
                ImageUrl = "categories/rubber_flooring.webp"
            },
            new Category() {
                Id = 2,
                Name = "Gym Flooring",
                ImageUrl = "categories/gym_flooring.webp"
            },
            new Category() {
                Id = 3,
                Name = "Foam Flooring",
                ImageUrl = "categories/foam_flooring.webp"
            },
            new Category() {
                Id = 4,
                Name = "Carpet Tiles",
                ImageUrl = "categories/carpet_tiles.webp"
            },
            new Category() {
                Id = 5,
                Name = "Vinyl Flooring",
                ImageUrl = "categories/vinyl_flooring.webp"
            },
            new Category() {
                Id = 6,
                Name = "Wood Flooring",
                ImageUrl = "categories/wood_flooring.webp"
            },
            new Category() {
                Id = 7,
                Name = "Rubber Mats",
                ImageUrl = "categories/rubber_mats.webp"
            },
            new Category() {
                Id = 8,
                Name = "Garage Flooring",
                ImageUrl = "categories/garage_flooring.webp"
            },
            new Category() {
                Id = 9,
                Name = "Dance Flooring",
                ImageUrl = "categories/dance_flooring.webp"
            },
            new Category() {
                Id = 10,
                Name = "Outdoor Flooring",
                ImageUrl = "categories/outdoor_flooring.webp"
            },
            new Category() {
                Id = 11,
                Name = "Turf & Artificial Grass",
                ImageUrl = "categories/turf_and_artificial_grass.webp"
            },
            new Category() {
                Id = 12,
                Name = "Composite Decking",
                ImageUrl = "categories/composite_decking.webp"
            }
        ]);

        await _context.SaveChangesAsync();
    }
}