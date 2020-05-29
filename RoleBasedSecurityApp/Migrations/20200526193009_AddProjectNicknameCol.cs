using Microsoft.EntityFrameworkCore.Migrations;

namespace RoleBasedSecurityApp.Migrations
{
    public partial class AddProjectNicknameCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nickname",
                table: "Projects",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nickname",
                table: "Projects");
        }
    }
}
