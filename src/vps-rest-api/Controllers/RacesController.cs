using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

using Models;

using Services;

namespace Controllers;

[ApiController]
[Route("races")]
public class RacesController(
    IRacesService racesService, ILapTimesService lapTimesService,
    IGenericRepository<Race> raceRepository, IGenericRepository<LapTime> lapTimeRepository
) : ControllerBase
{
    private readonly IRacesService _racesService = racesService;
    private readonly IGenericRepository<Race> _raceRepository = raceRepository;
    private readonly ILapTimesService _lapTimesService = lapTimesService;
    private readonly IGenericRepository<LapTime> _lapTimeRepository = lapTimeRepository;

    // Races

    [HttpGet]
    [SwaggerOperation(Summary = "Gets Races from the database.", Description = "Provides an endpoint for retrieving race data from the database.")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Race[]))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get([FromQuery] QueryParameters queryParameters)
    {
        PagedResponse<Race> response =
            await _raceRepository.GetAsync(
                queryParameters,
                $"{Request.Scheme}://{Request.Host}{Request.Path}",
                ["Circuit"]
            );
        return Ok(response);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Upserts Races into the database.", Description = "Provides an endpoint for upserting Races into the database.")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DatabaseInteractionResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DatabaseInteractionResult))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IResult Upsert([FromBody] Race[] races)
    {
        return _racesService.Upsert(races);
    }

    // LapTimes

    [HttpGet("lapTimes")]
    [SwaggerOperation(Summary = "Gets LapTimes from the database.", Description = "Provides an endpoint for retrieving LapTimes from the database.")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LapTime[]))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetLapTimes([FromQuery] QueryParameters queryParameters)
    {
        PagedResponse<LapTime> response =
            await _lapTimeRepository.GetAsync(
                queryParameters,
                $"{Request.Scheme}://{Request.Host}{Request.Path}",
                ["Race", "Driver"]
            );
        return Ok(response);
    }

    [HttpGet("lapTimes/{driverId}")]
    [SwaggerOperation(Summary = "Gets the Lap Times statistics for a Driver on every circuit.", Description = "Provides an endpoint for retrieving the average LapTimes for a Driver.")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DriverLapTimes))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IResult GetDriverLapTimes(int driverId, int? year = null)
    {
        DriverLapTimes[] driverLapTimes = _lapTimesService.GetDriverLapTimes(driverId, year);
        return TypedResults.Ok(driverLapTimes);
    }

    [HttpPost("lapTimes")]
    [SwaggerOperation(Summary = "Upserts LapTimes into the database.", Description = "Provides an endpoint for upserting LapTimes into the database.")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DatabaseInteractionResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DatabaseInteractionResult))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IResult Upsert([FromBody] LapTime[] lapTimes)
    {
        return _lapTimesService.Upsert(lapTimes);
    }
}