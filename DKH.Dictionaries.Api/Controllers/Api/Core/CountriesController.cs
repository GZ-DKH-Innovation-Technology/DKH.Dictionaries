using System.Net.Mime;
using DKH.Dictionaries.Application.Dto.Countries;
using DKH.Dictionaries.Application.Queries.Countries;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;

namespace DKH.Dictionaries.Api.Controllers.Api.Core;

public class CountriesController : BaseApiController
{
    [HttpGet]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResultDto<GetCountry>))]
    public async Task<IActionResult> Get([FromQuery] GetCountriesQuery request, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(request, cancellationToken));
    }

    [HttpGet("translations")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResultDto<GetCountryTranslation>))]
    public async Task<IActionResult> Translations([FromQuery] GetCountryTranslationsQuery request,
        CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(request, cancellationToken));
    }

    [HttpGet("int:id")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCountry))]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetCountryByIdQuery(id), cancellationToken));
    }
}