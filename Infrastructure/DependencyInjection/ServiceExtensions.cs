using Microsoft.Extensions.DependencyInjection;
using julius.Application.Services.Commands;
using julius.Application.Services.Handlers;
using julius.Application.Services.Queries;
using julius.Application.Interfaces;
using julius.Infrastructure.Services;
using julius.Infrastructure.Data;

namespace julius.Infrastructure.DependencyInjection;

public static class ServiceExtensions
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<UserQueryService>();
        services.AddScoped<UserCommandService>();
        services.AddScoped<UserHandlerService>();

        services.AddScoped<AuthCommandService>();
        services.AddScoped<AuthHandlerService>();
        
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<PasswordMigrationService>();
    }
} 