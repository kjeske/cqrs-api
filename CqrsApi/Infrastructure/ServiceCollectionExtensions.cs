using System.Reflection;

namespace CqrsApi.Infrastructure;

public static class ServiceCollectionRegistrationExtensions
{
    public static IServiceCollection AddAsImplementedInterfaces(this IServiceCollection services, Func<Type, bool> predicate, ServiceLifetime lifetime = ServiceLifetime.Transient) =>
        services.AddAsImplementedInterfaces(Assembly.GetExecutingAssembly(), predicate, lifetime);

    public static IServiceCollection AddAsImplementedInterfaces(this IServiceCollection services, Assembly assembly, Func<Type, bool> predicate, ServiceLifetime lifetime = ServiceLifetime.Transient)
    {
        assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract)
            .Where(predicate)
            .SelectMany(implementationType => implementationType.GetInterfaces()
                .Select(interfaceType => new ServiceDescriptor(interfaceType, implementationType, lifetime)))
            .ToList()
            .ForEach(services.Add);

        return services;
    }
}