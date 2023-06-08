using DKH.Dictionaries.Domain.Entities;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DKH.Dictionaries.Infrastructure.Persistence
{
    public class DictionaryDbContextInitializer
    {
        private readonly ILogger<DictionaryDbContextInitializer> _logger;
        private readonly DictionaryDbContext _context;
        private readonly List<string> _supportedCultures;

        public DictionaryDbContextInitializer(ILogger<DictionaryDbContextInitializer> logger,
            DictionaryDbContext context, IConfiguration configuration)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));

            _supportedCultures = configuration.GetSection("Localization:SupportedCultures").Get<List<string>>()!;
        }

        public async Task Initialise()
        {
            try
            {
                _ = await _context.Database.EnsureDeletedAsync();

                if (_context.Database.IsNpgsql())
                {
                    await _context.Database.MigrateAsync();
                }

                _ = await _context.Database.EnsureCreatedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database");
                throw;
            }
        }

        public async Task Seed()
        {
            try
            {
                await SeedLanguages();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database");
                throw;
            }
        }

        private async Task SeedLanguages()
        {
            if (!await _context.Languages.AnyAsync())
            {
                await _context.BulkInsertAsync(_supportedCultures.Select(culture => new LanguageEntity(Guid.NewGuid().ToString(), culture)));
                await _context.BulkSaveChangesAsync();
            }
        }
    }
}