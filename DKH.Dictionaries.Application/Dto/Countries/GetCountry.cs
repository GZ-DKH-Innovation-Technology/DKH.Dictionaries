using Volo.Abp.Application.Dtos;

namespace DKH.Dictionaries.Application.Dto.Countries;

public class GetCountry : EntityDto<string>
{
    /// <summary>
    ///     Name
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    ///     Native Name
    /// </summary>
    public string NativeName { get; set; } = default!;

    /// <summary>
    ///     ISO-3166-1 Alpha2 code
    /// </summary>
    public string TwoLetterCode { get; set; } = default!;

    /// <summary>
    ///     ISO-3166-1 Alpha3 code
    /// </summary>
    public string ThreeLetterCode { get; set; } = default!;

    /// <summary>
    ///     Numeric code
    /// </summary>
    public int NumericCode { get; set; }
}