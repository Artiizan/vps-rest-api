using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vpsrestapi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Circuits",
                columns: table => new
                {
                    circuitId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    circuitRef = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    location = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    country = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    lat = table.Column<decimal>(type: "decimal(9, 6)", nullable: false),
                    lng = table.Column<decimal>(type: "decimal(9, 6)", nullable: false),
                    alt = table.Column<int>(type: "INTEGER", nullable: false),
                    url = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Circuits", x => x.circuitId);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    driverId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    driverRef = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    number = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    code = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    forename = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    surname = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    dob = table.Column<DateTime>(type: "date", nullable: false),
                    nationality = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    url = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.driverId);
                });

            migrationBuilder.CreateTable(
                name: "DriverStandings",
                columns: table => new
                {
                    driverStandingsId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    raceId = table.Column<int>(type: "INTEGER", nullable: false),
                    driverId = table.Column<int>(type: "INTEGER", nullable: false),
                    points = table.Column<float>(type: "REAL", nullable: false),
                    position = table.Column<int>(type: "INTEGER", nullable: false),
                    positionText = table.Column<string>(type: "TEXT", nullable: true),
                    wins = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverStandings", x => x.driverStandingsId);
                });

            migrationBuilder.CreateTable(
                name: "LapTimes",
                columns: table => new
                {
                    raceId = table.Column<int>(type: "INTEGER", nullable: false),
                    driverId = table.Column<int>(type: "INTEGER", nullable: false),
                    lap = table.Column<int>(type: "INTEGER", nullable: false),
                    position = table.Column<int>(type: "INTEGER", nullable: false),
                    time = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false),
                    milliseconds = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LapTimes", x => new { x.raceId, x.driverId, x.lap });
                });

            migrationBuilder.CreateTable(
                name: "Races",
                columns: table => new
                {
                    raceId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    year = table.Column<int>(type: "INTEGER", nullable: false),
                    round = table.Column<int>(type: "INTEGER", nullable: false),
                    circuitId = table.Column<int>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    date = table.Column<DateTime>(type: "date", nullable: false),
                    time = table.Column<string>(type: "TEXT", maxLength: 8, nullable: false),
                    url = table.Column<string>(type: "TEXT", nullable: false),
                    fp1Date = table.Column<DateTime>(type: "date", nullable: true),
                    fp1Time = table.Column<string>(type: "TEXT", maxLength: 8, nullable: true),
                    fp2Date = table.Column<DateTime>(type: "date", nullable: true),
                    fp2Time = table.Column<string>(type: "TEXT", maxLength: 8, nullable: true),
                    fp3Date = table.Column<DateTime>(type: "date", nullable: true),
                    fp3Time = table.Column<string>(type: "TEXT", maxLength: 8, nullable: true),
                    qualiDate = table.Column<DateTime>(type: "date", nullable: true),
                    qualiTime = table.Column<string>(type: "TEXT", maxLength: 8, nullable: true),
                    sprintDate = table.Column<DateTime>(type: "date", nullable: true),
                    sprintTime = table.Column<string>(type: "TEXT", maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Races", x => x.raceId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Circuits");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "DriverStandings");

            migrationBuilder.DropTable(
                name: "LapTimes");

            migrationBuilder.DropTable(
                name: "Races");
        }
    }
}
