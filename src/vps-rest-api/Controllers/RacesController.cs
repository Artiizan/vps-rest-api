using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

using Models;

using Services;

namespace Controllers;

[ApiController]
[Route("races")]
public class RacesController : ControllerBase
{
    private readonly IRacesService _racesService;
    private readonly ILapTimesService _lapTimesService;

    public RacesController(IRacesService racesService, ILapTimesService lapTimesService)
    {
        _racesService = racesService;
        _lapTimesService = lapTimesService;
    }

    // Races

    [HttpPost]
    [SwaggerOperation(Summary = "Upserts Races into the database.", Description = "Provides an endpoint for upserting Races into the database.")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DatabaseInteractionResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DatabaseInteractionResult))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IResult Upsert([FromBody] Race[] races)
    {
        var result = _racesService.Upsert(races);
        return result;
    }

    // LapTimes
    [HttpPost("lapTimes")]
    [SwaggerOperation(Summary = "Upserts LapTimes into the database.", Description = "Provides an endpoint for upserting LapTimes into the database.")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DatabaseInteractionResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DatabaseInteractionResult))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IResult Upsert([FromBody] LapTime[] lapTimes)
    {
        var result = _lapTimesService.Upsert(lapTimes);
        return result;
    }
}