using Volo.Abp.Application.Dtos;

namespace DKH.Dictionaries.Application.Dto.States;

public class GetState : EntityDto<int>
{
    /// <summary>
    ///     Name
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    ///     Code
    /// </summary>
    public string Code { get; set; } = default!;

    /// <summary>
    ///     Type
    /// </summary>
    public string Type { get; set; } = default!;
}