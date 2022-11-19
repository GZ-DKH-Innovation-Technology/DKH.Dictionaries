using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DKH.Dictionaries.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        return services
            .AddAutoMapper(assembly)
            .AddValidatorsFromAssembly(assembly)
            .AddMediatR(assembly);
    }
}