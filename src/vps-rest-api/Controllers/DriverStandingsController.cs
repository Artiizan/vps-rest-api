using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

using Models;

using Services;

namespace Controllers;

[ApiController]
[Route("driverStandings")]
public class DriverStandingsController : ControllerBase
{
    private readonly IDriverStandingsService _driverStandingsService;

    public DriverStandingsController(IDriverStandingsService driverStandingsService)
    {
        _driverStandingsService = driverStandingsService;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Upserts driver standings into the database.", Description = "Provides an endpoint for upserting driver standings into the database.")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DatabaseInteractionResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DatabaseInteractionResult))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IResult Upsert([FromBody] DriverStanding[] driverStandings)
    {
        var result = _driverStandingsService.Upsert(driverStandings);
        return result;
    }
}