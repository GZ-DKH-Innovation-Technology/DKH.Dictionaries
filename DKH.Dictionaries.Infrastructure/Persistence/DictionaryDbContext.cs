using System.Reflection;
using DKH.Dictionaries.Domain.Entities;
using DKH.Dictionaries.Domain.Entities.Relationships;
using Microsoft.EntityFrameworkCore;

namespace DKH.Dictionaries.Infrastructure.Persistence;

public class DictionaryDbContext : DbContext
{
    public DictionaryDbContext(DbContextOptions<DictionaryDbContext> options) : base(options)
    {
    }

    protected DictionaryDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<CountryEntity> Countries { get; set; } = null!;
    public DbSet<CountryTranslationEntity> CountryTranslations { get; set; } = null!;
    public DbSet<StateEntity> States { get; set; } = null!;
    public DbSet<StateTranslationEntity> StateTranslations { get; set; } = null!;
    public DbSet<CityEntity> Cities { get; set; } = null!;
    public DbSet<CityTranslationEntity> CityTranslations { get; set; } = null!;
    public DbSet<CurrencyEntity> Currencies { get; set; } = null!;
    public DbSet<LanguageEntity> Languages { get; set; } = null!;

    #region Relationships

    public DbSet<CountryCurrencyRelationEntity> CountryCurrencyRelations { get; set; } = null!;

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.HasDefaultSchema("Dictionary");

        base.OnModelCreating(modelBuilder);
    }
}