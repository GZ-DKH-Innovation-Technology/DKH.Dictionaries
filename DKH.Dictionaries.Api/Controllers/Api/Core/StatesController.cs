using System.Net.Mime;
using DKH.Dictionaries.Application.Dto.States;
using DKH.Dictionaries.Application.Queries.States;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;

namespace DKH.Dictionaries.Api.Controllers.Api.Core;

public class StatesController : BaseApiController
{
    [HttpGet]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResultDto<GetState>))]
    public async Task<IActionResult> Get([FromQuery] GetStatesQuery request, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(request, cancellationToken));
    }

    [HttpGet("translations")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResultDto<GetStateTranslation>))]
    public async Task<IActionResult> Translations([FromQuery] GetStateTranslationsQuery request,
        CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(request, cancellationToken));
    }

    [HttpGet(nameof(GetById))]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetState))]
    public async Task<IActionResult> GetById([FromQuery] string id, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetStateByIdQuery(id), cancellationToken));
    }
}