using System.Diagnostics.CodeAnalysis;
using Volo.Abp.Domain.Entities.Auditing;

namespace DKH.Dictionaries.Domain.Entities;

[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local")]
public sealed class StateEntity : AuditedEntity<int>
{
    private readonly List<StateTranslationEntity> _translations = new();

    /// <summary>
    ///     Constructor for EF
    /// </summary>
    private StateEntity()
    {
    }

    public StateEntity(int id, string name, string code, string? type, int countryId) : base(id)
    {
        Name = name;
        Code = code;
        Type = type;
        CountryId = countryId;

        CreationTime = DateTime.UtcNow;

        _translations.Clear();
    }

    /// <summary>
    ///     Name
    /// </summary>
    public string Name { get; private set; } = default!;

    /// <summary>
    ///     Code
    /// </summary>
    public string Code { get; private set; } = default!;

    /// <summary>
    ///     Type
    /// </summary>
    public string? Type { get; private set; }

    public StateEntity AddTranslation(StateTranslationEntity translation)
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

    public IEnumerable<StateTranslationEntity> Translations => _translations;

    #endregion
}