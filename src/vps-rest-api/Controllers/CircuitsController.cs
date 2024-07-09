using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

using Models;

using Services;

namespace Controllers;

[ApiController]
[Route("circuits")]
public class CircuitsController : ControllerBase
{
    private readonly ICircuitsService _circuitsService;

    public CircuitsController(ICircuitsService circuitsService)
    {
        _circuitsService = circuitsService;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Upserts circuits into the database.", Description = "Provides an endpoint for upserting circuits into the database.")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DatabaseInteractionResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DatabaseInteractionResult))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IResult Upsert([FromBody] Circuit[] circuits)
    {
        var result = _circuitsService.Upsert(circuits);
        return result;
    }
}