using Volo.Abp.Application.Dtos;

namespace DKH.Dictionaries.Application.Dto.States;

public class GetStateTranslation : EntityDto<string>
{
    /// <summary>
    ///     Name
    /// </summary>
    public string Name { get; set; } = default!;
}