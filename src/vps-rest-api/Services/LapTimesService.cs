using Microsoft.EntityFrameworkCore;

using Models;

using Persistence;

namespace Services;

public interface ILapTimesService
{
    IResult Upsert(LapTime[] lapTimes);
    DriverLapTimes[] GetDriverLapTimes(int driverId, int? year);
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

    public DriverLapTimes[] GetDriverLapTimes(int driverId, int? year)
    {
        LapTime[] lapTimes = _db.LapTimes
        .Where(x => x.driverId == driverId)
        .Include(x => x.Race)
            .ThenInclude(race => race.Circuit)
        .Include(x => x.Driver)
        .ToArray();

        if (year.HasValue)
        {
            lapTimes = lapTimes.Where(x =>
                x.Race.year == year
            ).ToArray();
        }

        return lapTimes
            .GroupBy(x => x.Race.circuitId)
            .Select(x => new DriverLapTimes
            {
                Circuit = x.First().Race.Circuit,
                Driver = x.First().Driver,
                meanTime = TimeSpan.FromMilliseconds(x.Average(y => y.milliseconds)).ToString("mm\\:ss\\.fff"),
                meanMilliseconds = x.Average(y => y.milliseconds),
                fastestTime = TimeSpan.FromMilliseconds(x.Min(y => y.milliseconds)).ToString("mm\\:ss\\.fff"),
                fastestMilliseconds = x.Min(y => y.milliseconds)
            })
            .ToArray();
    }
}