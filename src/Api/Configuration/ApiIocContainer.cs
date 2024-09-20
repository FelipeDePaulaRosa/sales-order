using Api.Filters;
using Infrastructure.Database;
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
    }
    
    private static void RegisterDatabase(IServiceCollection services, IConfiguration configuration)
    {
        var connection = ConnectionDatabaseSettings.GetInstance(configuration);
        
        services.AddDbContext<DbContext>(options =>
        {
            options.UseSqlServer(connection.DefaultConnection);
        });
    }
}