using Microsoft.EntityFrameworkCore;

using Models;

using Persistence;

namespace Services;

public interface ICircuitsService
{
    IResult Upsert(Circuit[] circuits);
}

public class CircuitsService(DatabaseContext db) : ICircuitsService
{
    private readonly DatabaseContext _db = db;

    public IResult Upsert(Circuit[] circuits)
    {
        return DatasetOperations.UpsertData(
            circuits,
            (data) => _db.Circuits.UpsertRange(data).On(x => x.circuitId).Run(),
            "Circuit data has been upserted");
    }
}