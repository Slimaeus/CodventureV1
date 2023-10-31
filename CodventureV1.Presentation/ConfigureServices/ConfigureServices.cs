namespace CodventureV1.Presentation.ConfigureServices;

public static class ConfigureServices
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddPersistenceServices(configuration)
            .AddPresentationServices(configuration)
            .AddApplicationServices(configuration)
            .AddInfrastructureServices(configuration);

    public static Task<WebApplication> UseServices(this WebApplication app)
        => app
            .UsePresentationServices()
            .UsePersistenceServices();
}
