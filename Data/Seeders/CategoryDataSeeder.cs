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
                Name = "Rubber Flooring",
                ImageUrl = "categories/rubber_flooring.webp"
            },
            new Category() {
                Name = "Gym Flooring",
                ImageUrl = "categories/gym_flooring.webp"
            },
            new Category() {
                Name = "Foam Flooring",
                ImageUrl = "categories/foam_flooring.webp"
            },
            new Category() {
                Name = "Carpet Tiles",
                ImageUrl = "categories/carpet_tiles.webp"
            },
            new Category() {
                Name = "Vinyl Flooring",
                ImageUrl = "categories/vinyl_flooring.webp"
            },
            new Category() {
                Name = "Wood Flooring",
                ImageUrl = "categories/wood_flooring.webp"
            },
            new Category() {
                Name = "Rubber Mats",
                ImageUrl = "categories/rubber_mats.webp"
            },
            new Category() {
                Name = "Garage Flooring",
                ImageUrl = "categories/garage_flooring.webp"
            },
            new Category() {
                Name = "Dance Flooring",
                ImageUrl = "categories/dance_flooring.webp"
            },
            new Category() {
                Name = "Outdoor Flooring",
                ImageUrl = "categories/outdoor_flooring.webp"
            },
            new Category() {
                Name = "Turf & Artificial Grass",
                ImageUrl = "categories/turf_and_artificial_grass.webp"
            },
            new Category() {
                Name = "Composite Decking",
                ImageUrl = "categories/composite_decking.webp"
            }
        ]);

        await _context.SaveChangesAsync();
    }
}