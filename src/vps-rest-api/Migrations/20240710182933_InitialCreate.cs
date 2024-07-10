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
                    circuitId = table.Column<int>(type: "INTEGER", nullable: false),
                    circuitRef = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    location = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    country = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    lat = table.Column<decimal>(type: "decimal(9, 6)", nullable: false),
                    lng = table.Column<decimal>(type: "decimal(9, 6)", nullable: false),
                    alt = table.Column<int>(type: "INTEGER", nullable: true),
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
                    driverId = table.Column<int>(type: "INTEGER", nullable: false),
                    driverRef = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    number = table.Column<string>(type: "TEXT", nullable: true),
                    code = table.Column<string>(type: "TEXT", nullable: true),
                    forename = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    surname = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    dob = table.Column<DateTime>(type: "Date", nullable: false),
                    nationality = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    url = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.driverId);
                });

            migrationBuilder.CreateTable(
                name: "Races",
                columns: table => new
                {
                    raceId = table.Column<int>(type: "INTEGER", nullable: false),
                    year = table.Column<int>(type: "INTEGER", nullable: false),
                    round = table.Column<int>(type: "INTEGER", nullable: false),
                    circuitId = table.Column<int>(type: "INTEGER", nullable: true),
                    name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    date = table.Column<DateTime>(type: "Date", nullable: false),
                    time = table.Column<string>(type: "TEXT", nullable: true),
                    url = table.Column<string>(type: "TEXT", nullable: true),
                    fp1_date = table.Column<string>(type: "TEXT", nullable: true),
                    fp1_time = table.Column<string>(type: "TEXT", nullable: true),
                    fp2_date = table.Column<string>(type: "TEXT", nullable: true),
                    fp2_time = table.Column<string>(type: "TEXT", nullable: true),
                    fp3_date = table.Column<string>(type: "TEXT", nullable: true),
                    fp3_time = table.Column<string>(type: "TEXT", nullable: true),
                    quali_date = table.Column<string>(type: "TEXT", nullable: true),
                    quali_time = table.Column<string>(type: "TEXT", nullable: true),
                    sprint_date = table.Column<string>(type: "TEXT", nullable: true),
                    sprint_time = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Races", x => x.raceId);
                    table.ForeignKey(
                        name: "FK_Races_Circuits_circuitId",
                        column: x => x.circuitId,
                        principalTable: "Circuits",
                        principalColumn: "circuitId");
                });

            migrationBuilder.CreateTable(
                name: "DriverStandings",
                columns: table => new
                {
                    driverStandingsId = table.Column<int>(type: "INTEGER", nullable: false),
                    raceId = table.Column<int>(type: "INTEGER", nullable: true),
                    driverId = table.Column<int>(type: "INTEGER", nullable: true),
                    points = table.Column<float>(type: "REAL", nullable: true),
                    position = table.Column<int>(type: "INTEGER", nullable: true),
                    positionText = table.Column<string>(type: "TEXT", nullable: true),
                    wins = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverStandings", x => x.driverStandingsId);
                    table.ForeignKey(
                        name: "FK_DriverStandings_Drivers_driverId",
                        column: x => x.driverId,
                        principalTable: "Drivers",
                        principalColumn: "driverId");
                    table.ForeignKey(
                        name: "FK_DriverStandings_Races_raceId",
                        column: x => x.raceId,
                        principalTable: "Races",
                        principalColumn: "raceId");
                });

            migrationBuilder.CreateTable(
                name: "LapTimes",
                columns: table => new
                {
                    raceId = table.Column<int>(type: "INTEGER", nullable: false),
                    driverId = table.Column<int>(type: "INTEGER", nullable: false),
                    lap = table.Column<int>(type: "INTEGER", nullable: false),
                    position = table.Column<int>(type: "INTEGER", nullable: false),
                    time = table.Column<string>(type: "TEXT", maxLength: 8, nullable: false),
                    milliseconds = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LapTimes", x => new { x.raceId, x.driverId, x.lap });
                    table.ForeignKey(
                        name: "FK_LapTimes_Drivers_driverId",
                        column: x => x.driverId,
                        principalTable: "Drivers",
                        principalColumn: "driverId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LapTimes_Races_raceId",
                        column: x => x.raceId,
                        principalTable: "Races",
                        principalColumn: "raceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DriverStandings_driverId",
                table: "DriverStandings",
                column: "driverId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverStandings_raceId",
                table: "DriverStandings",
                column: "raceId");

            migrationBuilder.CreateIndex(
                name: "IX_LapTimes_driverId",
                table: "LapTimes",
                column: "driverId");

            migrationBuilder.CreateIndex(
                name: "IX_Races_circuitId",
                table: "Races",
                column: "circuitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DriverStandings");

            migrationBuilder.DropTable(
                name: "LapTimes");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Races");

            migrationBuilder.DropTable(
                name: "Circuits");
        }
    }
}
