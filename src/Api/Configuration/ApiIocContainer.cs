using Api.Filters;
using Application;
using Domain.Shared.Contracts;
using Infrastructure.Contexts;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api.Configuration;

public static class ApiIocContainer
{
    public static void RegisterControllers(this IServiceCollection services)
    {
        services
            .AddControllers(opt =>
                opt.Filters.Add(typeof(ExceptionFilter)));
    }
    
    public static void RegisterApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        RegisterDatabase(services, configuration);
        RegisterMediatR(services);
        RegisterDependencies(services);
    }
    
    private static void RegisterDatabase(IServiceCollection services, IConfiguration configuration)
    {
        var connection = ConnectionDatabaseSettings.GetInstance(configuration);
        
        services.AddDbContext<OrderDbContext>(options =>
        {
            options.UseSqlServer(connection.DefaultConnection);
        });
    }
    
    private static void RegisterMediatR(IServiceCollection services)
    {
        services.AddMediatR(opt => opt.RegisterServicesFromAssemblies(ApplicationAssemblyRef.Assembly));
    }
    
    private static void RegisterDependencies(IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IOrderRepository, OrderRepository>();
    }
}