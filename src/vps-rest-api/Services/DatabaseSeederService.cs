using System.Text.Json;

using Models;

using Persistence;

public class DatabaseSeederService(DatabaseContext db)
{
    private readonly DatabaseContext _db = db;

    public DatabaseInteractionResult SeedDatabase()
    {
        try
        {
            // Check if there is any data in the database
            if (_db.Circuits.Any() || _db.DriverStandings.Any() || _db.Drivers.Any() || _db.LapTimes.Any() || _db.Races.Any())
            {
                return new DatabaseInteractionResult
                {
                    Success = false,
                    Message = "The database already contains entries.",
                    RowsAffected = 0
                };
            }

            // Test the files in the dataset directory
            string currentDirectory = Directory.GetCurrentDirectory();
            if (!Directory.Exists("dataset"))
            {
                return new DatabaseInteractionResult
                {
                    Success = false,
                    Message = "The dataset directory does not exist.",
                    RowsAffected = 0
                };
            }

            // Read the files from the dataset directory          
            List<Circuit> circuits = ReadJsonFile<Circuit>("dataset/circuits.json");
            List<Race> races = ReadJsonFile<Race>("dataset/races.json");
            List<Driver> drivers = ReadJsonFile<Driver>("dataset/drivers.json");
            List<LapTime> lapTimes = ReadJsonFile<LapTime>("dataset/lap_times.json");
            List<DriverStanding> driverStandings = ReadJsonFile<DriverStanding>("dataset/driver_standings.json");

            // Add the data to the database'
            _db.Circuits.AddRange(circuits);
            _db.Races.AddRange(races);
            _db.Drivers.AddRange(drivers);
            _db.LapTimes.AddRange(lapTimes);
            _db.DriverStandings.AddRange(driverStandings);

            _db.SaveChanges();

            return new DatabaseInteractionResult
            {
                Success = true,
                Message = "The database has been seeded.",
                RowsAffected = new int[] { circuits.Count, driverStandings.Count, drivers.Count, lapTimes.Count, races.Count }.Sum()
            };

        }
        catch (Exception ex)
        {
            return new DatabaseInteractionResult
            {
                Success = false,
                Message = $"An error occurred while seeding the database: {ex.Message}",
                RowsAffected = 0
            };
        }
    }

    // Reusable method to read in JSON files and return the deserialized object
    public static List<T> ReadJsonFile<T>(string path)
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
    }
}