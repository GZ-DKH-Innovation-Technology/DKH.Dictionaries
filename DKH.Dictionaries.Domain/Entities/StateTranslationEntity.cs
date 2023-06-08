using System.Diagnostics.CodeAnalysis;
using Volo.Abp.Domain.Entities.Auditing;

namespace DKH.Dictionaries.Domain.Entities;

[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local")]
public class StateTranslationEntity : AuditedEntity<string>
{
    /// <summary>
    ///     Constructor for EF
    /// </summary>
    private StateTranslationEntity()
    {
        
    }

    public StateTranslationEntity(string id, string name, string stateId, string languageId) : base(id)
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
    public string StateId { get; private set; }

    /// <summary>
    ///     Country
    /// </summary>
    public StateEntity State { get; private set; } = default!;

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