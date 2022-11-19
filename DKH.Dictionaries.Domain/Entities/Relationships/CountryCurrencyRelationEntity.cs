using System.Diagnostics.CodeAnalysis;
using Volo.Abp.Domain.Entities.Auditing;

namespace DKH.Dictionaries.Domain.Entities.Relationships;

[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local")]
public sealed class CountryCurrencyRelationEntity : AuditedEntity<int>
{
    /// <summary>
    ///     Constructor for EF
    /// </summary>
    private CountryCurrencyRelationEntity()
    {
    }

    public CountryCurrencyRelationEntity(int id, int countryId, int currencyId, CountryEntity? country = null,
        CurrencyEntity? currency = null) : base(id)
    {
        CountryId = countryId;
        CurrencyId = currencyId;

        CreationTime = DateTime.UtcNow;
    }

    /// <summary>
    ///     Country Id
    /// </summary>
    public int CountryId { get; private set; }

    /// <summary>
    ///     Country
    /// </summary>
    public CountryEntity Country { get; private set; } = default!;

    /// <summary>
    ///     Currency Id
    /// </summary>
    public int CurrencyId { get; private set; }

    /// <summary>
    ///     Currency
    /// </summary>
    public CurrencyEntity Currency { get; private set; } = default!;
}