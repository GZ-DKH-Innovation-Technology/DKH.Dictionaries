using DKH.Dictionaries.Application.Queries.OverPass.Dto;
using DKH.Dictionaries.Application.Queries.OverPass;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DKH.Dictionaries.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddHttpClient<IRequestHandler<GetOverPassQuery, GetOverPassResult>, GetOverPassQueryHandler>();

        return services
            .AddAutoMapper(assembly)
            .AddValidatorsFromAssembly(assembly)
            .AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));
    }
}