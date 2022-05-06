using Microsoft.EntityFrameworkCore.Migrations;

namespace EstateReporting.Database.Migrations.SqlServer
{
    public partial class BIDataModelChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "responsecodes",
                columns: table => new
                {
                    ResponseCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_responsecodes", x => x.ResponseCode);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "responsecodes");
        }
    }
}
