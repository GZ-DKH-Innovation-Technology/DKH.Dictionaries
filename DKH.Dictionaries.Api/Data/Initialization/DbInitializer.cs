using DKH.Dictionaries.Domain.Entities.Relationships;
using DKH.Dictionaries.Domain.Entities;
using DKH.Dictionaries.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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

            #region languages

            if (!await context.Languages.AnyAsync(cancellationToken))
                await context.AddRangeAsync(Languages(supportedCultures), cancellationToken: cancellationToken);

            #endregion

            #region currencies

            var currencies = await Currencies(environment.ContentRootPath, cancellationToken);

            if (!await context.Currencies.AnyAsync(cancellationToken) && currencies != null)
                await context.AddRangeAsync(currencies, cancellationToken: cancellationToken);

            #endregion

            #region countries

            var countries = await Countries(environment.ContentRootPath, cancellationToken);

            if (!await context.Countries.AnyAsync(cancellationToken) && countries != null)
                await context.AddRangeAsync(countries, cancellationToken: cancellationToken);

            var countryTranslations = await CountryTranslations(environment.ContentRootPath, cancellationToken);

            if (!await context.CountryTranslations.AnyAsync(cancellationToken) && countryTranslations != null)
                await context.AddRangeAsync(countryTranslations, cancellationToken: cancellationToken);

            #region Relation

            var countryCurrencyRelations =
                await CountryCurrencyRelations(environment.ContentRootPath, cancellationToken);

            if (!await context.CountryCurrencyRelations.AnyAsync(cancellationToken) && countryCurrencyRelations != null)
                await context.AddRangeAsync(countryCurrencyRelations, cancellationToken: cancellationToken);

            #endregion

            #endregion

            #region states

            var states = await States(environment.ContentRootPath, cancellationToken);

            if (!await context.States.AnyAsync(cancellationToken) && states != null)
                await context.AddRangeAsync(states, cancellationToken: cancellationToken);

            var stateTranslations = await StateTranslations(environment.ContentRootPath, cancellationToken);

            if (!await context.StateTranslations.AnyAsync(cancellationToken) && stateTranslations != null)
                await context.AddRangeAsync(stateTranslations, cancellationToken: cancellationToken);

            #endregion

            #region cities

            var cities = await Cities(environment.ContentRootPath, cancellationToken);

            if (!await context.Cities.AnyAsync(cancellationToken) && cities != null)
                await context.AddRangeAsync(cities, cancellationToken: cancellationToken);

            var cityTranslations = await CityTranslations(environment.ContentRootPath, cancellationToken);

            if (!await context.CityTranslations.AnyAsync(cancellationToken) && cityTranslations != null)
                await context.AddRangeAsync(cityTranslations, cancellationToken: cancellationToken);

            #endregion

            await context.SaveChangesAsync(cancellationToken);
        }
    }
    #region Languages

    private static List<LanguageEntity> Languages(IEnumerable<string> supportedCultures)
    {
        return supportedCultures.Select(culture => new LanguageEntity(0, culture)).ToList();
    }

    #endregion

    #region Currencies

    private static async Task<List<CurrencyEntity>?> Currencies(string contentRootPath,
        CancellationToken cancellationToken)
    {
        var json = await File.ReadAllTextAsync(Path.Combine(contentRootPath, "Data/JsonFiles/Currencies.json"),
            cancellationToken);
        return JsonConvert.DeserializeObject<List<CurrencyEntity>>(json);
    }

    #endregion

    #region Countries

    private static async Task<List<CountryEntity>?> Countries(string contentRootPath,
        CancellationToken cancellationToken)
    {
        var countriesJson =
            await File.ReadAllTextAsync(Path.Combine(contentRootPath, "Data/JsonFiles/Countries.json"), cancellationToken);
        return JsonConvert.DeserializeObject<List<CountryEntity>>(countriesJson);
    }

    private static async Task<List<CountryTranslationEntity>?> CountryTranslations(string contentRootPath,
        CancellationToken cancellationToken)
    {
        var json = await File.ReadAllTextAsync(Path.Combine(contentRootPath, "Data/JsonFiles/CountryTranslations.json"),
            cancellationToken);
        return JsonConvert.DeserializeObject<List<CountryTranslationEntity>>(json);
    }

    private static async Task<List<CountryCurrencyRelationEntity>?> CountryCurrencyRelations(string contentRootPath,
        CancellationToken cancellationToken)
    {
        var json =
            await File.ReadAllTextAsync(
                Path.Combine(contentRootPath, "Data/JsonFiles/CountryCurrencyRelation.json"),
                cancellationToken);
        return JsonConvert.DeserializeObject<List<CountryCurrencyRelationEntity>>(json);
    }

    #endregion

    #region States

    private static async Task<List<StateEntity>?> States(string contentRootPath,
        CancellationToken cancellationToken)
    {
        var json = await File.ReadAllTextAsync(Path.Combine(contentRootPath, "Data/JsonFiles/States.json"), cancellationToken);
        return JsonConvert.DeserializeObject<List<StateEntity>>(json);
    }

    private static async Task<List<StateTranslationEntity>?> StateTranslations(string contentRootPath,
        CancellationToken cancellationToken)
    {
        var json = await File.ReadAllTextAsync(Path.Combine(contentRootPath, "Data/JsonFiles/StateTranslations.json"),
            cancellationToken);
        return JsonConvert.DeserializeObject<List<StateTranslationEntity>>(json);
    }

    #endregion

    #region Cities

    private static async Task<List<CityEntity>?> Cities(string contentRootPath,
        CancellationToken cancellationToken)
    {
        var json = await File.ReadAllTextAsync(Path.Combine(contentRootPath, "Data/JsonFiles/Cities.json"), cancellationToken);
        return JsonConvert.DeserializeObject<List<CityEntity>>(json);
    }

    private static async Task<List<CityTranslationEntity>?> CityTranslations(string contentRootPath,
        CancellationToken cancellationToken)
    {
        var json = await File.ReadAllTextAsync(Path.Combine(contentRootPath, "Data/JsonFiles/CityTranslations.json"),
            cancellationToken);
        return JsonConvert.DeserializeObject<List<CityTranslationEntity>>(json);
    }

    #endregion
}