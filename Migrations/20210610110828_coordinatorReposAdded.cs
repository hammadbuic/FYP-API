using Microsoft.EntityFrameworkCore.Migrations;

namespace Academic_project_manager_WebAPI.Migrations
{
    public partial class coordinatorReposAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "reposId",
                table: "Coordinator",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "reposName",
                table: "Coordinator",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "reposUrl",
                table: "Coordinator",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "reposId",
                table: "Coordinator");

            migrationBuilder.DropColumn(
                name: "reposName",
                table: "Coordinator");

            migrationBuilder.DropColumn(
                name: "reposUrl",
                table: "Coordinator");
        }
    }
}
