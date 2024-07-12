using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

using Models;

using Services;

namespace Controllers;

[ApiController]
[Route("drivers")]
public class DriversController(
    IDriversService driversService, IDriverStandingsService driverStandingsService,
    IGenericRepository<Driver> driverRepository, IGenericRepository<DriverStanding> driverStandingRepository
) : ControllerBase
{
    private readonly IDriversService _driversService = driversService;
    private readonly IGenericRepository<Driver> _driverRepository = driverRepository;
    private readonly IDriverStandingsService _driverStandingsService = driverStandingsService;
    private readonly IGenericRepository<DriverStanding> _driverStandingRepository = driverStandingRepository;

    // Drivers

    [HttpGet]
    [SwaggerOperation(Summary = "Gets all Drivers from the database.", Description = "Provides an endpoint for retrieving all Drivers from the database.")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Driver[]))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get([FromQuery] QueryParameters queryParameters)
    {
        PagedResponse<Driver> response =
            await _driverRepository.GetAsync(queryParameters, $"{Request.Scheme}://{Request.Host}{Request.Path}");
        return Ok(response);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Upserts Drivers into the database.", Description = "Provides an endpoint for upserting Drivers into the database.")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DatabaseInteractionResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DatabaseInteractionResult))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IResult Upsert([FromBody] Driver[] drivers)
    {
        return _driversService.Upsert(drivers);
    }

    // Driver Standings

    [HttpGet("standings")]
    [SwaggerOperation(Summary = "Get the driver standings information from the database, enriched with related data.", Description = "Provides an endpoint for retrieving driver standings information from the database, enriched with related data.")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DriverStanding[]))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetStandings([FromQuery] QueryParameters queryParameters)
    {
        PagedResponse<DriverStanding> response =
            await _driverStandingRepository.GetAsync(
                queryParameters,
                $"{Request.Scheme}://{Request.Host}{Request.Path}",
                ["Race", "Driver"]
            );
        return Ok(response);
    }

    [HttpPost("standings")]
    [SwaggerOperation(Summary = "Upserts driver standings into the database.", Description = "Provides an endpoint for upserting driver standings into the database.")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DatabaseInteractionResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DatabaseInteractionResult))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IResult UpsertStandings([FromBody] DriverStanding[] driverStandings)
    {
        return _driverStandingsService.Upsert(driverStandings);
    }
}