using System.Diagnostics.CodeAnalysis;
using Volo.Abp.Domain.Entities.Auditing;

namespace DKH.Dictionaries.Domain.Entities;

[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local")]
public sealed class CountryTranslationEntity : AuditedEntity<int>
{
    /// <summary>
    ///     Constructor for EF
    /// </summary>
    private CountryTranslationEntity()
    {
    }

    public CountryTranslationEntity(int id, string name, int countryId, int languageId) : base(id)
    {
        Name = name;
        CountryId = countryId;
        LanguageId = languageId;

        CreationTime = DateTime.UtcNow;
    }

    /// <summary>
    ///     Name
    /// </summary>
    public string Name { get; private set; } = default!;

    public CountryTranslationEntity AddCountry(CountryEntity country)
    {
        Country = country;
        CountryId = country.Id;

        return this;
    }

    public CountryTranslationEntity AddCountryId(int countryId)
    {
        CountryId = countryId;
        return this;
    }

    public CountryTranslationEntity AddLanguage(LanguageEntity language)
    {
        Language = language;
        LanguageId = language.Id;

        return this;
    }

    public CountryTranslationEntity AddLanguageId(int languageId)
    {
        LanguageId = languageId;
        return this;
    }

    #region Navigation Properties

    /// <summary>
    ///     Country Id
    /// </summary>
    public int CountryId { get; private set; }

    /// <summary>
    ///     Country
    /// </summary>
    public CountryEntity Country { get; private set; } = default!;

    /// <summary>
    ///     Language Id
    /// </summary>
    public int LanguageId { get; private set; }

    /// <summary>
    ///     Language
    /// </summary>
    public LanguageEntity Language { get; private set; } = default!;

    #endregion
}