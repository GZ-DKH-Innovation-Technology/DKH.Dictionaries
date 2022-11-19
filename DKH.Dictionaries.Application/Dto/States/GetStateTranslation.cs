using Volo.Abp.Application.Dtos;

namespace DKH.Dictionaries.Application.Dto.States;

public class GetStateTranslation : EntityDto<int>
{
    /// <summary>
    ///     Name
    /// </summary>
    public string Name { get; set; } = default!;
}