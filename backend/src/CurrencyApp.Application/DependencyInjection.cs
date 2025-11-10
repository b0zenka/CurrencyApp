using CurrencyApp.Application.Abstractions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddValidatorsFromAssembly(assembly);

        foreach (var type in assembly.GetTypes())
        {
            foreach (var iface in type.GetInterfaces())
            {
                if (iface.IsGenericType && iface.GetGenericTypeDefinition() == typeof(IQueryHandler<,>))
                    services.AddScoped(iface, type);
            }
        }

        return services;
    }
}
