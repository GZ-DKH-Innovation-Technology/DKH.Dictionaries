using Volo.Abp.Application.Dtos;

namespace DKH.Dictionaries.Application.Dto.Languages;

public class GetLanguage : EntityDto<int>
{
    /// <summary>
    ///     culture name format (e.g. en-US).
    /// </summary>
    public string CultureName { get; set; } = default!;

    public string NativeName { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
    public string EnglishName { get; set; } = default!;

    /// <summary>
    ///     Gets the ISO 639-1 two-letter code for the language.
    /// </summary>
    public string TwoLetterLanguageName { get; set; } = default!;

    /// <summary>
    ///     Gets the ISO 639-2 three-letter code for the language.
    /// </summary>
    public string ThreeLetterLanguageName { get; set; } = default!;
}