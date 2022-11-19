using System.Net.Mime;
using DKH.Dictionaries.Application.Dto.Currencies;
using DKH.Dictionaries.Application.Queries.Currencies;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;

namespace DKH.Dictionaries.Api.Controllers.Api.Core;

public class CurrenciesController : BaseApiController
{
    [HttpGet]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResultDto<GetCurrency>))]
    public async Task<IActionResult> Get([FromQuery] GetCurrenciesQuery request, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(request, cancellationToken));
    }
}