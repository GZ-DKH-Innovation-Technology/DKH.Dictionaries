using System.Net.Mime;
using DKH.Dictionaries.Application.Dto.Languages;
using DKH.Dictionaries.Application.Queries.Languages;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;

namespace DKH.Dictionaries.Api.Controllers.Api.Core;

public class LanguagesController : BaseApiController
{
    [HttpGet]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResultDto<GetLanguage>))]
    public async Task<IActionResult> Get([FromQuery] GetLanguagesQuery request, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(request, cancellationToken));
    }

    [HttpGet(nameof(GetById))]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetLanguage))]
    public async Task<IActionResult> GetById([FromQuery] string id, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetLanguageByIdQuery(id), cancellationToken));
    }
}