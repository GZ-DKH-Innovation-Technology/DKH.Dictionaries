using System.Diagnostics.CodeAnalysis;
using Volo.Abp.Domain.Entities.Auditing;

namespace DKH.Dictionaries.Domain.Entities;

[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local")]
public class CountryTranslationEntity : AuditedEntity<string>
{
    /// <summary>
    ///     Constructor for EF
    /// </summary>
    private CountryTranslationEntity()
    {
    }

    public CountryTranslationEntity(string id, string name, string countryId, string languageCode) : base(id)
    {
        Name = name;
        CountryId = countryId;
        LanguageCode = languageCode;

        CreationTime = DateTime.UtcNow;
    }

    /// <summary>
    ///     Name
    /// </summary>
    public string Name { get; private set; } = default!;
    
    /// <summary>
    ///     Language Code
    /// </summary>
    public string LanguageCode { get; private set; } = string.Empty;

    public CountryTranslationEntity AddCountry(CountryEntity country)
    {
        Country = country;
        CountryId = country.Id;

        return this;
    }

    public CountryTranslationEntity AddCountryId(string countryId)
    {
        CountryId = countryId;
        return this;
    }

    #region Navigation Properties

    /// <summary>
    ///     Country Id
    /// </summary>
    public string CountryId { get; private set; } = string.Empty;

    /// <summary>
    ///     Country
    /// </summary>
    public CountryEntity Country { get; private set; } = default!;
    
    #endregion
}