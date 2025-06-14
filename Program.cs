using PortfolioWebAPI;
using PortfolioWebAPI.Data;
using PortfolioWebAPI.Data.Seeders;
using PortfolioWebAPI.Interfaces;
using PortfolioWebAPI.Middleware;
using PortfolioWebAPI.Services;
using PortfolioWebAPI.Settings;
using PortfolioWebAPI.Tools;

//--Create App Builder
var builder = WebApplication.CreateBuilder(args);

//--System Services
builder.Services.AddControllers();
builder.Services
    .AddOpenApi();

//--Custom Services
builder.Services.AddDbContext<PortfolioDbContext>(ServiceLifetime.Transient);
builder.Services
    .AddTransient<SeedingService>()
    .AddTransient<IDataSeeder, TrendingProductDataSeeder>()
    .AddTransient<IDataSeeder, CategoryDataSeeder>()
    .AddTransient<IDataSeeder, FAQsDataSeeder>();

//--Configuration
builder.Configuration.AddJsonFile("Settings/appsettings.json");
builder.Configuration.AddJsonFile($"Settings/appsettings.{builder.Environment.EnvironmentName}.json");

//--Configure IOptions
builder.Services.Configure<SiteSettingOptions>(
    builder.Configuration.GetSection(SiteSettingOptions.SiteSettings));

//--Add controller options.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<CommonTestingActionFilter>();
});

//--Build Application
var app = builder.Build();

//--Configure Application
if (app.Environment.IsDevelopment() || app.Environment.IsDockerSolo() || app.Environment.IsDockerCompose())
{
    app.MapOpenApi();
    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "/openapi/v1.json";
    });
}
//app.UseHttpsRedirection();
app.UseMiddleware<UnhandledExceptionMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.UseCors(cors => cors
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin());

//--Seed data for in-memory database.
using (IServiceScope scope = app.Services.CreateScope())
{
    ILogger<Program>? logger = scope.ServiceProvider.GetService<ILogger<Program>>();

    //--Refresh DB
    try
    {
        PortfolioDbContext context = scope.ServiceProvider.GetRequiredService<PortfolioDbContext>();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        SeedingService seedingService = scope.ServiceProvider.GetRequiredService<SeedingService>();
        await seedingService.SeedDatabaseAsync();
    }
    catch (Exception ex)
    {
        logger?.LogWarning("SEED ERROR, Ignoring. Message: {exMessage}", ex.Message);
    }
}

//--App Start!
app.Run();
