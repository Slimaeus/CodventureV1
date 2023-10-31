using CodventureV1.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CodventureV1.Presentation.ConfigureServices;

public static class ConfigurePersistence
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTriggeredDbContextPool<ApplicationDbContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            options.UseTriggers(config => config.AddAssemblyTriggers(Persistence.AssemblyProvider.ExecutingAssembly));
        });

        services.AddScoped<ApplicationDbContextInitializer>();

        return services;
    }

    public static async Task<WebApplication> UsePersistenceServices(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            using var scope = app.Services.CreateScope();
            var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
            await initializer.InitialiseAsync();
            await initializer.SeedAsync();
        }
        return app;
    }
}
