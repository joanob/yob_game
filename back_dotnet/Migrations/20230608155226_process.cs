using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace back_dotnet.Migrations
{
    public partial class process : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Production",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProductionBuildingId = table.Column<int>(type: "int", nullable: false),
                    ProductionProcessId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Start = table.Column<long>(type: "bigint", nullable: false),
                    End = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Production", x => new { x.UserId, x.ProductionBuildingId });
                    table.ForeignKey(
                        name: "FK_Production_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Production");
        }
    }
}
