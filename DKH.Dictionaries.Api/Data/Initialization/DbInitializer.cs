using DKH.Dictionaries.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DKH.Dictionaries.Api.Data.Initialization;

public static class DbInitializer
{
    public static async Task DictionaryDbInitialize(this IServiceProvider services,
        IWebHostEnvironment environment,
        IEnumerable<string> supportedCultures,
        CancellationToken cancellationToken = default)
    {
        using var scope = services.CreateScope();
        await using var context = scope.ServiceProvider.GetService<DictionaryDbContext>();

        if (context != null)
        {
            await context.Database.EnsureDeletedAsync(cancellationToken);
            await context.Database.MigrateAsync(cancellationToken);
            await context.Database.EnsureCreatedAsync(cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}