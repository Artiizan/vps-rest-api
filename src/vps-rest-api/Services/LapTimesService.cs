using Microsoft.EntityFrameworkCore;

using Models;

using Persistence;

namespace Services;

public interface ILapTimesService
{
    IResult Upsert(LapTime[] lapTimes);
}

public class LapTimesService(DatabaseContext db) : ILapTimesService
{
    private readonly DatabaseContext _db = db;

    public IResult Upsert(LapTime[] lapTimes)
    {
        return DatasetOperations.UpsertData(
            lapTimes,
            (data) => _db.LapTimes.UpsertRange(data).On(x => new { x.raceId, x.driverId, x.lap }).Run(),
            "Lap times has been upserted");
    }
}