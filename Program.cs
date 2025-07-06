using PortfolioWebAPI;
using PortfolioWebAPI.Data;
using PortfolioWebAPI.Middleware;
using PortfolioWebAPI.Settings;
using PortfolioWebAPI.Tools;
using Scalar.AspNetCore;

//--Create App Builder
var builder = WebApplication.CreateBuilder(args);

//--System Services
builder.Services.AddControllers();
builder.Services
    .AddMemoryCache()
    .AddOpenApi()
    .AddAutoMapper(typeof(AutoMapperProfile));

//--Custom Services
builder.Services.AddDbContext<PortfolioDbContext>(ServiceLifetime.Transient);

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
app.UseRouting();
app.UseCors(cors => cors
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin());
app.UseMiddleware<UnhandledExceptionMiddleware>();
app.UseMiddleware<RateLimiterMiddleware>();
app.UseMiddleware<ApiKeyMiddleware>();
if (app.Environment.IsProduction() || app.Environment.IsGoogleCloudRun())
{
    app.UseHttpsRedirection();
}
else if (app.Environment.IsDevelopment() || app.Environment.IsDockerCompose())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.UseAuthorization();
app.MapControllers();


//--App Start!
app.Run();
