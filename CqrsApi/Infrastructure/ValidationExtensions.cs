using FluentValidation;
using MediatR;

namespace CqrsApi.Infrastructure;

public static class ServiceCollectionValidationExtensions
{
    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddAsImplementedInterfaces(t => t.IsAssignableTo(typeof(IValidator)));
        return services;
    }
}