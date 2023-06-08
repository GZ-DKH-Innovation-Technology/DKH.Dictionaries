using System.Diagnostics.CodeAnalysis;
using Volo.Abp.Domain.Entities.Auditing;

namespace DKH.Dictionaries.Domain.Entities;

[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local")]
public class CityTranslationEntity : AuditedEntity<string>
{
    /// <summary>
    ///     Constructor for EF
    /// </summary>
    private CityTranslationEntity()
    {
    }

    public CityTranslationEntity(string id, string name, string cityId, string languageId) : base(id)
    {
        Name = name;
        CityId = cityId;
        LanguageId = languageId;

        CreationTime = DateTime.UtcNow;
    }

    /// <summary>
    ///     Name
    /// </summary>
    public string Name { get; private set; } = default!;

    #region Navigation Properties

    /// <summary>
    ///     Country Id
    /// </summary>
    public string CityId { get; private set; }

    /// <summary>
    ///     Country
    /// </summary>
    public CityEntity City { get; private set; } = default!;

    /// <summary>
    ///     Language Id
    /// </summary>
    public string LanguageId { get; private set; }

    /// <summary>
    ///     Language
    /// </summary>
    public LanguageEntity Language { get; private set; } = default!;

    #endregion
}