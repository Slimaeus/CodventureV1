using CodventureV1.Domain.Common.Interfaces;
using CodventureV1.Domain.Players;
using CodventureV1.Domain.Roles;
using CodventureV1.Infrastructure.Players;
using CodventureV1.Infrastructure.Repositories.Commands;
using CodventureV1.Infrastructure.Repositories.Queries;
using CodventureV1.Infrastructure.UnitOfWorks;
using CodventureV1.Persistence;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CodventureV1.Presentation.ConfigureServices;

public static class ConfigureInfrastructure
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<Player, PlayerRole>(options =>
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
            .AddRoles<PlayerRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IPlayerQueryRepository, PlayerQueryRepository>();
        services.AddScoped<IQueryRepositoryFactory, QueryRepositoryFactory>();
        services.AddScoped<ICommandRepositoryFactory, CommandRepositoryFactory>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
