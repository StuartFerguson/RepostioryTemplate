using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EstateReporting.Database.Migrations.SqlServer
{
    public partial class addcalendar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "calendar",
                columns: table => new
                {
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DayOfWeek = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DayOfWeekNumber = table.Column<int>(type: "int", nullable: false),
                    DayOfWeekShort = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MonthNameLong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MonthNameShort = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MonthNumber = table.Column<int>(type: "int", nullable: false),
                    WeekNumber = table.Column<int>(type: "int", nullable: true),
                    WeekNumberString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false),
                    YearWeekNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_calendar", x => x.Date);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "calendar");
        }
    }
}
