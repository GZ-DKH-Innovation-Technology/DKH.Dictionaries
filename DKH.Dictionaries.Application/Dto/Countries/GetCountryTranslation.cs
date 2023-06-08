using Volo.Abp.Application.Dtos;

namespace DKH.Dictionaries.Application.Dto.Countries;

public class GetCountryTranslation : EntityDto<string>
{
    public string Name { get; set; } = default!;
}