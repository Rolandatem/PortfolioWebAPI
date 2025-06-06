using PortfolioWebAPI.Data;
using PortfolioWebAPI.Data.Seeders;
using PortfolioWebAPI.Interfaces;
using PortfolioWebAPI.Services;

//--Create App Builder
var builder = WebApplication.CreateBuilder(args);

//--System Services
builder.Services.AddControllers();
builder.Services
    .AddOpenApi();

//--Custom Services
builder.Services.AddDbContext<PortfolioDbContext>();
builder.Services
    .AddTransient<SeedingService>()
    .AddTransient<IDataSeeder, TrendingProductDataSeeder>();

//--Build Application
var app = builder.Build();

//--Configure Application
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "/openapi/v1.json";
    });
}
//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

//--Seed data for in-memory database.
using (IServiceScope scope = app.Services.CreateScope())
{
    SeedingService seedingService = scope.ServiceProvider.GetRequiredService<SeedingService>();
    await seedingService.SeedDatabaseAsync();
}

//--App Start!
app.Run();
