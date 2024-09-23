using Api.Filters;
using Application;
using CrossCutting.FluentValidationNotifications;
using Domain.Shared.Contracts;
using Domain.Shared.Validations;
using FluentValidation;
using Infrastructure.Contexts;
using Infrastructure.Database;
using Infrastructure.Events;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Configuration;

public static class ApiIocContainer
{
    public static void RegisterControllers(this IServiceCollection services)
    {
        services
            .AddControllers(opt =>
            {
                opt.Filters.Add(typeof(ExceptionFilter));
                opt.Filters.Add(typeof(FluentValidationNotificationFilter));
                opt.Filters.Add(typeof(DomainEventFilter));
            });
    }

    public static void RegisterApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        RegisterDatabase(services, configuration);
        RegisterValidators(services);
        RegisterMediatR(services);
        RegisterDependencies(services);
    }

    private static void RegisterDatabase(IServiceCollection services, IConfiguration configuration)
    {
        var connection = ConnectionDatabaseSettings.GetInstance(configuration);

        services.AddDbContext<OrderDbContext>(options => { options.UseSqlServer(connection.DefaultConnection); });
    }

    private static void RegisterValidators(IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(ApplicationAssemblyRef.Assembly, includeInternalTypes: true);
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }

    private static void RegisterMediatR(IServiceCollection services)
    {
        services.AddMediatR(opt => opt.RegisterServicesFromAssemblies(ApplicationAssemblyRef.Assembly));
    }

    private static void RegisterDependencies(IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IFluentValidationNotificationContext, FluentValidationNotificationContext>();
        services.AddTransient<OrderDbContext>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IDomainEventNotification<Guid>, DomainEventNotification<Guid>>();
        services.AddScoped<IEventPublisher<Guid>, EventPublisher<Guid>>();
    }
}