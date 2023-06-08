using System.Diagnostics.CodeAnalysis;
using DKH.Dictionaries.Domain.Entities.Relationships;
using Volo.Abp.Domain.Entities.Auditing;

namespace DKH.Dictionaries.Domain.Entities;

/// <summary>
///     Currency ISO 4217
///     https://en.wikipedia.org/wiki/ISO_4217
/// </summary>
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local")]
public class CurrencyEntity : AuditedEntity<string>
{
    private readonly List<CountryCurrencyRelationEntity> _countries = new();

    /// <summary>
    ///     Constructor for EF
    /// </summary>
    private CurrencyEntity()
    {
    }

    public CurrencyEntity(string id, string name, string symbol, string code) : base(id)
    {
        Name = name;
        Code = code;
        Symbol = symbol;

        CreationTime = DateTime.UtcNow;

        _countries.Clear();
    }

    /// <summary>
    ///     Name
    /// </summary>
    public string Name { get; private set; } = default!;

    /// <summary>
    ///     IsoCode
    /// </summary>
    public string Code { get; private set; } = default!;

    /// <summary>
    ///     Symbol
    /// </summary>
    public string Symbol { get; private set; } = default!;

    #region Navigation Properties

    public IEnumerable<CountryCurrencyRelationEntity> Countries => _countries;

    #endregion

    public CurrencyEntity AddCountry(CountryCurrencyRelationEntity country)
    {
        if (country == null) throw new ArgumentNullException(nameof(country));
        _countries.Add(country);

        return this;
    }
}