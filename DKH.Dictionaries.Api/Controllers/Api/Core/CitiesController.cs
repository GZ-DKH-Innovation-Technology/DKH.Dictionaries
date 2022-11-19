using System.Net.Mime;
using DKH.Dictionaries.Application.Dto.Cities;
using DKH.Dictionaries.Application.Queries.Cities;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;

namespace DKH.Dictionaries.Api.Controllers.Api.Core;

public class CitiesController : BaseApiController
{
    [HttpGet]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResultDto<GetCity>))]
    public async Task<IActionResult> Get([FromQuery] GetCitiesQuery request, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(request, cancellationToken));
    }

    [HttpGet("translations")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResultDto<GetCityTranslation>))]
    public async Task<IActionResult> Translations([FromQuery] GetCityTranslationsQuery request,
        CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(request, cancellationToken));
    }

    [HttpGet("int:id")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCity))]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetCityByIdQuery(id), cancellationToken));
    }
}