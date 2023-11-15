using Carter;
using CodventureV1.Domain.Players;
using CodventureV1.Persistence;
using Microsoft.AspNetCore.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CodventureV1.Presentation.ConfigureServices;

public static class ConfigurePresentation
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {

        });

        services.AddIdentityApiEndpoints<Player>(options =>
        {
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 8;

            options.User.RequireUniqueEmail = true;

            options.SignIn.RequireConfirmedAccount = false;
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;

            options.ClaimsIdentity.UserNameClaimType = ClaimTypes.Name;
            options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
            options.ClaimsIdentity.EmailClaimType = ClaimTypes.Email;
        })
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.Configure<JsonOptions>(options =>
        {
            //options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.SerializerOptions.IncludeFields = true;
            options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });

        services.AddAuthorization();

        services.AddCarter();

        return services;
    }

    public static WebApplication UsePresentationServices(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            //options.SupportedSubmitMethods(SubmitMethod.Get);
            //options.DisplayRequestDuration();

            options.EnableDeepLinking();
            options.EnableFilter();
            options.EnablePersistAuthorization();
            options.EnableTryItOutByDefault();
            options.EnableValidator();
        });

        app.UseHttpsRedirection();

        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        app.MapIdentityApi<Player>();

        app.MapGet("/weatherforecast", () =>
        {
            var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
                .ToArray();
            return forecast;
        })
        .WithName("GetWeatherForecast")
        .WithOpenApi();

        app.MapCarter();

        return app;
    }
}
internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
