using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

using Models;

using Services;

namespace Controllers;

[ApiController]
[Route("circuits")]
public class CircuitsController(ICircuitsService circuitsService, IGenericRepository<Circuit> circuitRepository) : ControllerBase
{
    private readonly ICircuitsService _circuitsService = circuitsService;
    private readonly IGenericRepository<Circuit> _circuitRepository = circuitRepository;

    [HttpGet]
    [SwaggerOperation(Summary = "Retrieves specified circuits from the database.", Description = "Provides an endpoint for retrieving all circuits from the database.")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Circuit[]))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get([FromQuery] QueryParameters queryParameters)
    {
        PagedResponse<Circuit> response =
            await _circuitRepository.GetAsync(queryParameters, $"{Request.Scheme}://{Request.Host}{Request.Path}");
        return Ok(response);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Upserts circuits into the database.", Description = "Provides an endpoint for upserting circuits into the database.")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DatabaseInteractionResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DatabaseInteractionResult))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IResult Upsert([FromBody] Circuit[] circuits)
    {
        IResult result = _circuitsService.Upsert(circuits);
        return result;
    }
}