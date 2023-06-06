using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DynamicSun.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "weather_archive",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Time = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Temperature = table.Column<double>(type: "double precision", nullable: false),
                    RelativeHumidity = table.Column<int>(type: "integer", nullable: true),
                    DewPointTemperature = table.Column<double>(type: "double precision", nullable: false),
                    AtmosphericPressure = table.Column<int>(type: "integer", nullable: true),
                    WindDirection = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    WindSpeed = table.Column<int>(type: "integer", nullable: true),
                    Cloudiness = table.Column<int>(type: "integer", nullable: true),
                    CloudBase = table.Column<int>(type: "integer", nullable: true),
                    Visibility = table.Column<int>(type: "integer", nullable: true),
                    WeatherPhenomena = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_weather_archive", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "weather_archive");
        }
    }
}
