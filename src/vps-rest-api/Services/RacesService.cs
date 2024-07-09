using Microsoft.EntityFrameworkCore;

using Models;
using Persistence;

namespace Services;

public interface IRacesService
{
    IResult Upsert(Race[] races);
}

public class RacesService : IRacesService
{
    private readonly DatabaseContext _db;

    public RacesService(DatabaseContext db)
    {
        _db = db;
    }

    public IResult Upsert(Race[] races)
    {
        return DatasetOperations.UpsertData(
            races,
            (data) => _db.Races.UpsertRange(data).On(x => x.raceId).Run(), 
            "Race data has been upserted");
    }
}