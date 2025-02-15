using Microsoft.EntityFrameworkCore;

using Models;
using Persistence;

namespace Services;

public interface IDriversService
{
    IResult Upsert(Driver[] drivers);
}

public class DriversService(DatabaseContext db) : IDriversService
{
    private readonly DatabaseContext _db = db;

    public IResult Upsert(Driver[] drivers)
    {
        return DatasetOperations.UpsertData(
            drivers,
            (data) => _db.Drivers.UpsertRange(data).On(x => x.driverId).Run(),
            "Driver data has been upserted");
    }
}