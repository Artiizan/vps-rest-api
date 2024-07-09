using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

using Models;

using Services;

namespace Controllers;

[ApiController]
[Route("lapTimes")]
public class LapTimesController : ControllerBase
{
    private readonly ILapTimesService _lapTimesService;

    public LapTimesController(ILapTimesService lapTimesService)
    {
        _lapTimesService = lapTimesService;
    }

    [HttpPost]
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