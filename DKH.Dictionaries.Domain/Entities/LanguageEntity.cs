using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Volo.Abp.Domain.Entities.Auditing;

namespace DKH.Dictionaries.Domain.Entities;

[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local")]
public sealed class LanguageEntity : AuditedEntity<int>
{
    #region Constructor

    /// <summary>
    ///     Constructor for EF
    /// </summary>
    private LanguageEntity()
    {
    }

    #endregion

    public LanguageEntity(int id, string cultureName) : base(id)
    {
        var culture = string.IsNullOrEmpty(cultureName)
            ? CultureInfo.InvariantCulture
            : CultureInfo.GetCultureInfo(cultureName);

        CultureName = culture.Name;
        NativeName = culture.NativeName;
        DisplayName = culture.DisplayName;
        EnglishName = culture.EnglishName;
        TwoLetterLanguageName = culture.TwoLetterISOLanguageName.ToUpper();
        ThreeLetterLanguageName = culture.ThreeLetterISOLanguageName.ToUpper();

        CreationTime = DateTime.UtcNow;
    }

    /// <summary>
    ///     culture name format (e.g. en-US).
    /// </summary>
    public string CultureName { get; private set; } = default!;

    public string NativeName { get; private set; } = default!;
    public string DisplayName { get; private set; } = default!;
    public string EnglishName { get; private set; } = default!;

    /// <summary>
    ///     Gets the ISO 639-1 two-letter code for the language.
    /// </summary>
    public string TwoLetterLanguageName { get; private set; } = default!;

    /// <summary>
    ///     Gets the ISO 639-2 three-letter code for the language.
    /// </summary>
    public string ThreeLetterLanguageName { get; private set; } = default!;

    public LanguageEntity Update(string cultureName)
    {
        var culture = CultureInfo.GetCultureInfo(cultureName);

        CultureName = culture.Name;
        NativeName = culture.NativeName;
        DisplayName = culture.DisplayName;
        EnglishName = culture.EnglishName;
        TwoLetterLanguageName = culture.TwoLetterISOLanguageName;
        ThreeLetterLanguageName = culture.ThreeLetterISOLanguageName;

        LastModificationTime = DateTime.UtcNow;
        return this;
    }
}