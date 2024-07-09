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

    public RacesController(IRacesService racesService)
    {
        _racesService = racesService;
    }

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
}