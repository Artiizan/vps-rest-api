using Microsoft.EntityFrameworkCore;

using Models;

using Persistence;

namespace Services;

public interface IDriverStandingsService
{
    IResult Upsert(DriverStanding[] driverStandings);
}

public class DriverStandingsService(DatabaseContext db) : IDriverStandingsService
{
    private readonly DatabaseContext _db = db;

    public IResult Upsert(DriverStanding[] driverStandings)
    {
        return DatasetOperations.UpsertData(
            driverStandings,
            (data) => _db.DriverStandings.UpsertRange(data).On(x => x.driverStandingsId).Run(),
            "Driver Standings data has been upserted");
    }
}