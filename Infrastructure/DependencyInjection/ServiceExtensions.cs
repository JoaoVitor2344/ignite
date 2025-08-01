using Microsoft.Extensions.DependencyInjection;
using ignite.Application.Services.Commands;
using ignite.Application.Services.Handlers;
using ignite.Application.Services.Queries;
using ignite.Application.Interfaces;
using ignite.Infrastructure.Services;
using ignite.Infrastructure.Data;

namespace ignite.Infrastructure.DependencyInjection;

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
        services.AddScoped<GoalQueryService>();
        services.AddScoped<GoalCommandService>();
        services.AddScoped<GoalHandlerService>();
    }
} 