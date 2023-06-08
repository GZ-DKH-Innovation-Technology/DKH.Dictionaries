using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DKH.Dictionaries.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        return services
            .AddAutoMapper(assembly)
            .AddValidatorsFromAssembly(assembly)
            .AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));
    }
}