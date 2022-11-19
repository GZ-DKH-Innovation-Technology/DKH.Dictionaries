using Volo.Abp.Application.Dtos;

namespace DKH.Dictionaries.Application.Dto.Cities;

public class GetCityTranslation : EntityDto<int>
{
    /// <summary>
    ///     Name
    /// </summary>
    public string Name { get; set; } = default!;
}