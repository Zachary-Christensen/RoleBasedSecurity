using Microsoft.EntityFrameworkCore.Migrations;

namespace RoleBasedSecurityApp.Migrations
{
    public partial class AddProjectCounter121 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectID",
                table: "Counters",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
