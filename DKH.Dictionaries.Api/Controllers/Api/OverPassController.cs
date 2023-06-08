using System.Net.Mime;
using DKH.Dictionaries.Application.Queries.OverPass;
using DKH.Dictionaries.Application.Queries.OverPass.Dto;
using Microsoft.AspNetCore.Mvc;

namespace DKH.Dictionaries.Api.Controllers.Api;

public class OverPassController : BaseApiController
{
    [HttpGet]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOverPassResult))]
    public async Task<IActionResult> Get([FromQuery] GetOverPassQuery request, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(request, cancellationToken));
    }

    [HttpGet(nameof(GetCountries))]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOverPassResult))]
    public async Task<IActionResult> GetCountries([FromQuery] string url, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(url)) url = "https://overpass-api.de/api/interpreter";

        var query = @"
            [out:json];
            rel[""boundary""=""administrative""][""admin_level""=""2""];
            out tags;
        ";
        return Ok(await Mediator.Send(new GetOverPassQuery(url, query), cancellationToken));
    }

    [HttpGet(nameof(GetCapitals))]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOverPassResult))]
    public async Task<IActionResult> GetCapitals([FromQuery] string url, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(url)) url = "https://overpass-api.de/api/interpreter";

        var query = @"
            [out:json];
            rel[""boundary""=""administrative""][""admin_level""=""2""];
            node(r:""admin_centre"");
            out tags;
        ";
        return Ok(await Mediator.Send(new GetOverPassQuery(url, query), cancellationToken));
    }

    [HttpGet(nameof(GetStates))]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOverPassResult))]
    public async Task<IActionResult> GetStates([FromQuery] string url, string country, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(url)) url = "https://overpass-api.de/api/interpreter";

        var query = string.Format(@"
            [out:json];
            area[""ISO3166-1""=""{0}""]->.country;
            rel(area.country)[""boundary""=""administrative""][""admin_level""=""4""];
            out tags;
        ", country);
        return Ok(await Mediator.Send(new GetOverPassQuery(url, query), cancellationToken));
    }

    [HttpGet(nameof(GetStatesDistricts))]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOverPassResult))]
    public async Task<IActionResult> GetStatesDistricts([FromQuery] string url, string country, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(url)) url = "https://overpass-api.de/api/interpreter";

        var query = string.Format(@"
            [out:json];
            area[""ISO3166-1""=""{0}""]->.country;
            rel(area.country)[""boundary""=""administrative""][""admin_level""=""6""];
            out tags;
        ", country);
        return Ok(await Mediator.Send(new GetOverPassQuery(url, query), cancellationToken));
    }

    [HttpGet(nameof(GetCities))]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOverPassResult))]
    public async Task<IActionResult> GetCities([FromQuery] string url, string country, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(url)) url = "https://overpass-api.de/api/interpreter";

        var query = string.Format(@"
            [out:json];
            area[""ISO3166-1""=""{0}""]->.country;
            node[place=""city""](area.country);
            out tags;
        ", country);
        return Ok(await Mediator.Send(new GetOverPassQuery(url, query), cancellationToken));
    }

    [HttpGet(nameof(GetCitiesWithTownsAndVillage))]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOverPassResult))]
    public async Task<IActionResult> GetCitiesWithTownsAndVillage([FromQuery] string url, string country, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(url)) url = "https://overpass-api.de/api/interpreter";

        var query = string.Format(@"
            [out:json];
            area[""ISO3166-1""=""{0}""]->.country;
            ( 
                node[place=""city""](area.country);
                node[place=""town""](area.country);
                node[place=""village""](area.country);
            );
            out tags;
        ", country);
        return Ok(await Mediator.Send(new GetOverPassQuery(url, query), cancellationToken));
    }
}