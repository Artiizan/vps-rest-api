using System.Text.Json;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

using Persistence;

using Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
{
    // dependency injection

    builder.Services.AddSwaggerGen(c => { c.EnableAnnotations(); });
    builder.Services.AddControllers();

    // database context
    builder.Services.AddSingleton<DatabaseContext>();

    // api endpoint services
    builder.Services.AddSingleton<DatabaseSeederService>();
    builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    builder.Services.AddSingleton<ICircuitsService, CircuitsService>();
    builder.Services.AddSingleton<IDriverStandingsService, DriverStandingsService>();
    builder.Services.AddSingleton<IDriversService, DriversService>();
    builder.Services.AddSingleton<ILapTimesService, LapTimesService>();
    builder.Services.AddSingleton<IRacesService, RacesService>();
}

WebApplication app = builder.Build();
{
    // configure request pipeline

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "VPS - REST API v1");
        c.DocumentTitle = "VPS - REST API";
        c.RoutePrefix = string.Empty; // serve the Swagger UI at the app's root
    });

    app.UseAuthorization();
    app.MapControllers();

    app.UseExceptionHandler(c => c.Run(async context =>
    {
        Exception? exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        string result = JsonSerializer.Serialize(new { error = exception?.Message });
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(result);
    }));

}

// Ensure the database is created and migrations are applied
using (IServiceScope scope = app.Services.CreateScope())
{
    try
    {
        DatabaseContext db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        db.Database.EnsureCreated();
        db.Database.Migrate();
    }
    catch (Exception ex)
    {
        // TODO: Implement a more robust loggin framework
        Console.WriteLine($"Error: {ex.Message}");
    }
}

app.Run();