using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

using Models;

using Services;

namespace Controllers;

[ApiController]
[Route("drivers")]
public class DriversController : ControllerBase
{
    private readonly IDriversService _driversService;

    public DriversController(IDriversService driversService)
    {
        _driversService = driversService;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Upserts Drivers into the database.", Description = "Provides an endpoint for upserting Drivers into the database.")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DatabaseInteractionResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DatabaseInteractionResult))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IResult Upsert([FromBody] Driver[] drivers)
    {
        var result = _driversService.Upsert(drivers);
        return result;
    }
}