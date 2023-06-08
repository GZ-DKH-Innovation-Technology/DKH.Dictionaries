using DKH.Dictionaries.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DKH.Dictionaries.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<DictionaryDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DictionaryDbContext"),
                builder => builder.MigrationsAssembly(typeof(DictionaryDbContext).Assembly.FullName)));

        services.AddScoped<DictionaryDbContextInitializer>();

        return services;
    }
}