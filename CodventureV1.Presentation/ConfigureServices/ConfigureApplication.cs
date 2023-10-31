using FluentValidation;

namespace CodventureV1.Presentation.ConfigureServices;

public static class ConfigureApplication
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblies(Application.AssemblyProvider.ExecutingAssembly);
        });
        services.AddAutoMapper(Application.AssemblyProvider.ExecutingAssembly);
        services.AddValidatorsFromAssembly(Application.AssemblyProvider.ExecutingAssembly);
        return services;
    }
}
