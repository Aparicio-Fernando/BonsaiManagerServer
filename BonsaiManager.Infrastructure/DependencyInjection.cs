using BonsaiManager.Application.Interfaces;
using BonsaiManager.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BonsaiManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddHttpContextAccessor();
        services.AddScoped<IHttpContextService, HttpContextService>();

        return services;
    }
}