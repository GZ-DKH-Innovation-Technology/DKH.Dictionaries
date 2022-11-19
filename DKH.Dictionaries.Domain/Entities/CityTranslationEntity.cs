using System.Diagnostics.CodeAnalysis;
using Volo.Abp.Domain.Entities.Auditing;

namespace DKH.Dictionaries.Domain.Entities;

[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local")]
public sealed class CityTranslationEntity : AuditedEntity<int>
{
    /// <summary>
    ///     Constructor for EF
    /// </summary>
    private CityTranslationEntity()
    {
    }

    public CityTranslationEntity(int id, string name, int cityId, int languageId) : base(id)
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
    public int CityId { get; private set; }

    /// <summary>
    ///     Country
    /// </summary>
    public CityEntity City { get; private set; } = default!;

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