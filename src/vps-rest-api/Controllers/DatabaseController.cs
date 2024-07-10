using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

using Models;

namespace Controllers;

[ApiController]
[Route("database")]
public class DatabaseController(DatabaseSeederService seederService) : ControllerBase
{
    private readonly DatabaseSeederService _seederService = seederService;

    [HttpGet]
    [SwaggerOperation(Summary = "Seeds the database with the provided test data on the local filesystem.", Description = "Provides an endpoint for seeding the database after initial creation for rapid testing. This is ideal for testing with the full data set as it can be too large to paste into a REST program.")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DatabaseInteractionResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DatabaseInteractionResult))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IResult SeedDatabase()
    {
        DatabaseInteractionResult result = _seederService.SeedDatabase();
        return result.Success ? TypedResults.Ok(result) : TypedResults.Json(result, statusCode: 500);
    }
}