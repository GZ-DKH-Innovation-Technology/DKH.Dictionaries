using Volo.Abp.Application.Dtos;

namespace DKH.Dictionaries.Application.Dto.Currencies;

public class GetCurrency : EntityDto<int>
{
    /// <summary>
    ///     Name
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    ///     IsoCode
    /// </summary>
    public string Code { get; set; } = default!;

    /// <summary>
    ///     Symbol
    /// </summary>
    public string Symbol { get; set; } = default!;
}