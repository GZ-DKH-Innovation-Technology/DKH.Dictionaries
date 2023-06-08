using Volo.Abp.Application.Dtos;

namespace DKH.Dictionaries.Application.Dto.Cities;

public class GetCity : EntityDto<string>
{
    /// <summary>
    ///     Name
    /// </summary>
    public string Name { get; set; } = default!;
}