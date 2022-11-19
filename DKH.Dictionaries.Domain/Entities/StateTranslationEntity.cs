using System.Diagnostics.CodeAnalysis;
using Volo.Abp.Domain.Entities.Auditing;

namespace DKH.Dictionaries.Domain.Entities;

[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local")]
public sealed class StateTranslationEntity : AuditedEntity<int>
{
    /// <summary>
    ///     Constructor for EF
    /// </summary>
    private StateTranslationEntity()
    {
    }

    public StateTranslationEntity(int id, string name, int stateId, int languageId) : base(id)
    {
        Name = name;
        StateId = stateId;
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
    public int StateId { get; private set; }

    /// <summary>
    ///     Country
    /// </summary>
    public StateEntity State { get; private set; } = default!;

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