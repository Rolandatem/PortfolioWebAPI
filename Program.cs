using Microsoft.Extensions.Options;
using PortfolioWebAPI.Data;
using PortfolioWebAPI.Middleware;
using PortfolioWebAPI.Settings;
using PortfolioWebAPI.Tools;

//--Create App Builder
var builder = WebApplication.CreateBuilder(args);

//--System Services
builder.Services.AddControllers();
builder.Services
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
// if (app.Environment.IsDevelopment() || app.Environment.IsDockerSolo() || app.Environment.IsDockerCompose())
// {
    app.MapOpenApi();
    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "/openapi/v1.json";
    });
// }
//app.UseHttpsRedirection();
app.UseMiddleware<UnhandledExceptionMiddleware>();
app.UseAuthorization();
app.MapControllers();

IOptions<SiteSettingOptions> siteSettings = app.Services.GetRequiredService<IOptions<SiteSettingOptions>>();

app.UseCors(cors => cors
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin());

//--App Start!
app.Run();
