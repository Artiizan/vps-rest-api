using Microsoft.EntityFrameworkCore;
using Models;

namespace Persistence;

public class DatabaseContext : DbContext
{
    // DbSets for access to the database tables
    public DbSet<Circuit> Circuits { get; set; }
    public DbSet<DriverStanding> DriverStandings { get; set; }
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<LapTime> LapTimes { get; set; }
    public DbSet<Race> Races { get; set; }

    // For simplicity, we will use a SQLite database
    // In production, we may want to use a more robust relational database
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source=dataset.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // As there is no key defined in the LapTime class, 
        // we will use the combination of RaceId, DriverId, and Lap as the key
        modelBuilder.Entity<LapTime>()
            .HasKey(lt => new { lt.raceId, lt.driverId, lt.lap });
    }
}