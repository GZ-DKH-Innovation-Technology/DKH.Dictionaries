using System.Diagnostics.CodeAnalysis;
using DKH.Dictionaries.Domain.Entities.Relationships;
using DKH.Dictionaries.Domain.Enums;
using Volo.Abp.Domain.Entities.Auditing;

namespace DKH.Dictionaries.Domain.Entities;

/// <summary>
///     List of ISO 3166 country codes
///     https://en.wikipedia.org/wiki/List_of_ISO_3166_country_codes
/// </summary>
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local")]
public sealed class CountryEntity : AuditedEntity<int>
{
    private readonly List<CountryCurrencyRelationEntity> _currencies = new();
    private readonly List<CountryTranslationEntity> _translations = new();

    /// <summary>
    ///     Constructor for EF
    /// </summary>
    private CountryEntity()
    {
    }

    public CountryEntity(
        int id,
        string name,
        string nativeName,
        CountryTwoLetterCodeEnum twoLetterCode,
        CountryThreeLetterCodeEnum threeLetterCode,
        int numericCode,
        string phoneCode,
        string capital,
        string tld,
        string region,
        string subregion) : base(id)
    {
        Name = name;
        NativeName = nativeName;
        TwoLetterCode = twoLetterCode;
        ThreeLetterCode = threeLetterCode;
        NumericCode = numericCode;
        PhoneCode = phoneCode;
        Capital = capital;
        Tld = tld;
        Region = region;
        Subregion = subregion;

        CreationTime = DateTime.UtcNow;

        _currencies.Clear();
        _translations.Clear();
    }

    /// <summary>
    ///     Name
    /// </summary>
    public string Name { get; private set; } = default!;

    /// <summary>
    ///     Native Name
    /// </summary>
    public string NativeName { get; private set; } = default!;

    /// <summary>
    ///     Alpha-2 code (ISO 3166-1)
    /// </summary>
    public CountryTwoLetterCodeEnum TwoLetterCode { get; private set; }

    /// <summary>
    ///     Alpha-3 code (ISO 3166-1)
    /// </summary>
    public CountryThreeLetterCodeEnum ThreeLetterCode { get; private set; }

    /// <summary>
    ///     Numeric code (ISO 3166-1)
    /// </summary>
    public int NumericCode { get; private set; }

    /// <summary>
    ///     Phone code
    /// </summary>
    public string PhoneCode { get; private set; } = default!;

    /// <summary>
    ///     Capital
    /// </summary>
    public string Capital { get; private set; } = default!;

    /// <summary>
    ///     Country code top-level domain
    /// </summary>
    public string Tld { get; private set; } = default!;

    /// <summary>
    ///     Region
    /// </summary>
    public string Region { get; private set; } = default!;

    /// <summary>
    ///     Subregion
    /// </summary>
    public string Subregion { get; private set; } = default!;

    public CountryEntity Update(
        int id,
        string name,
        string nativeName,
        CountryTwoLetterCodeEnum twoLetterCode,
        CountryThreeLetterCodeEnum threeLetterCode,
        int numericCode,
        string phoneCode,
        string capital,
        string tld,
        string region,
        string subregion)
    {
        Id = id;
        Name = name;
        NativeName = nativeName;
        TwoLetterCode = twoLetterCode;
        ThreeLetterCode = threeLetterCode;
        NumericCode = numericCode;
        PhoneCode = phoneCode;
        Capital = capital;
        Tld = tld;
        Region = region;
        Subregion = subregion;

        LastModificationTime = DateTime.UtcNow;
        return this;
    }

    public CountryEntity AddCurrency(CountryCurrencyRelationEntity currency)
    {
        if (currency == null) throw new ArgumentNullException(nameof(currency));
        _currencies.Add(currency);

        return this;
    }

    public CountryEntity AddTranslation(CountryTranslationEntity translation)
    {
        if (translation == null) throw new ArgumentNullException(nameof(translation));
        _translations.Add(translation.AddCountry(this));

        return this;
    }

    #region Navigation Properties

    public IEnumerable<CountryCurrencyRelationEntity> Currencies => _currencies;
    public IEnumerable<CountryTranslationEntity> Translations => _translations;

    #endregion
}