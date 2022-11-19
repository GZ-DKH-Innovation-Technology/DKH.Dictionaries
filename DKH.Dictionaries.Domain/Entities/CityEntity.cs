using System.Diagnostics.CodeAnalysis;
using Volo.Abp.Domain.Entities.Auditing;

namespace DKH.Dictionaries.Domain.Entities;

[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local")]
public sealed class CityEntity : AuditedEntity<int>
{
    private readonly List<CityTranslationEntity> _translations = new();

    /// <summary>
    ///     Constructor for EF
    /// </summary>
    private CityEntity()
    {
    }

    public CityEntity(int id, string name, int countryId, int stateId) : base(id)
    {
        Name = name;
        CountryId = countryId;
        StateId = stateId;

        CreationTime = DateTime.UtcNow;

        _translations.Clear();
    }

    /// <summary>
    ///     Name
    /// </summary>
    public string Name { get; private set; } = default!;

    public CityEntity AddTranslation(CityTranslationEntity translation)
    {
        if (translation == null) throw new ArgumentNullException(nameof(translation));
        _translations.Add(translation);

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
    ///     Country Id
    /// </summary>
    public int StateId { get; private set; }

    /// <summary>
    ///     Country
    /// </summary>
    public StateEntity State { get; private set; } = default!;

    public IEnumerable<CityTranslationEntity> Translations => _translations;

    #endregion
}