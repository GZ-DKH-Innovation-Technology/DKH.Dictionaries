using System.Diagnostics.CodeAnalysis;
using Volo.Abp.Domain.Entities.Auditing;

namespace DKH.Dictionaries.Domain.Entities.Relationships;

[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local")]
public sealed class CountryCurrencyRelationEntity : AuditedEntity<string>
{
    /// <summary>
    ///     Constructor for EF
    /// </summary>
    private CountryCurrencyRelationEntity()
    {
    }

    public CountryCurrencyRelationEntity(string id, string countryId, string currencyId) : base(id)
    {
        CountryId = countryId;
        CurrencyId = currencyId;

        CreationTime = DateTime.UtcNow;
    }

    public CountryCurrencyRelationEntity(string id, CountryEntity country, CurrencyEntity currency) : this(id, country.Id, currency.Id)
    {
        Country = country;
        Currency = currency;
    }

    /// <summary>
    ///     Country Id
    /// </summary>
    public string CountryId { get; private set; } = string.Empty;

    /// <summary>
    ///     Country
    /// </summary>
    public CountryEntity Country { get; private set; } = default!;

    /// <summary>
    ///     Currency Id
    /// </summary>
    public string CurrencyId { get; private set; } = string.Empty;

    /// <summary>
    ///     Currency
    /// </summary>
    public CurrencyEntity Currency { get; private set; } = default!;
}