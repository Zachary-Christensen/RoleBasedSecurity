using Microsoft.EntityFrameworkCore.Migrations;

namespace RoleBasedSecurityApp.Migrations
{
    public partial class AddProjectCounterM2M : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Counters_Projects_ProjectID",
                table: "Counters");

            migrationBuilder.DropIndex(
                name: "IX_Counters_ProjectID",
                table: "Counters");

            migrationBuilder.DropColumn(
                name: "ProjectID",
                table: "Counters");

            migrationBuilder.CreateTable(
                name: "ProjectCounters",
                columns: table => new
                {
                    ProjectID = table.Column<int>(nullable: false),
                    CounterID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectCounters", x => new { x.ProjectID, x.CounterID });
                    table.UniqueConstraint("AK_ProjectCounters_CounterID_ProjectID", x => new { x.CounterID, x.ProjectID });
                    table.ForeignKey(
                        name: "FK_ProjectCounters_Counters_CounterID",
                        column: x => x.CounterID,
                        principalTable: "Counters",
                        principalColumn: "CounterID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectCounters_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectCounters");

            migrationBuilder.AddColumn<int>(
                name: "ProjectID",
                table: "Counters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Counters_ProjectID",
                table: "Counters",
                column: "ProjectID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Counters_Projects_ProjectID",
                table: "Counters",
                column: "ProjectID",
                principalTable: "Projects",
                principalColumn: "ProjectID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
