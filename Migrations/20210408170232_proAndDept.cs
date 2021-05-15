using Microsoft.EntityFrameworkCore.Migrations;

namespace Academic_project_manager_WebAPI.Migrations
{
    public partial class proAndDept : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "department",
                table: "Supervisors");

            migrationBuilder.DropColumn(
                name: "program",
                table: "Supervisors");

            migrationBuilder.DropColumn(
                name: "program",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "program",
                table: "Coordinator",
                newName: "section");

            migrationBuilder.AddColumn<string>(
                name: "department",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "program",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "department",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "program",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "section",
                table: "Coordinator",
                newName: "program");

            migrationBuilder.AddColumn<string>(
                name: "department",
                table: "Supervisors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "program",
                table: "Supervisors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "program",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
