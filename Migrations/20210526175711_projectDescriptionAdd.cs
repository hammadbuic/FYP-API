using Microsoft.EntityFrameworkCore.Migrations;

namespace Academic_project_manager_WebAPI.Migrations
{
    public partial class projectDescriptionAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "projectDescription",
                table: "Project",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "projectDescription",
                table: "Project");
        }
    }
}
