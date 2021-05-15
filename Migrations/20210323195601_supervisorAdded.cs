using Microsoft.EntityFrameworkCore.Migrations;

namespace Academic_project_manager_WebAPI.Migrations
{
    public partial class supervisorAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "supervisorId",
                table: "groups",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_groups_supervisorId",
                table: "groups",
                column: "supervisorId");

            migrationBuilder.AddForeignKey(
                name: "FK_groups_Supervisors_supervisorId",
                table: "groups",
                column: "supervisorId",
                principalTable: "Supervisors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_groups_Supervisors_supervisorId",
                table: "groups");

            migrationBuilder.DropIndex(
                name: "IX_groups_supervisorId",
                table: "groups");

            migrationBuilder.DropColumn(
                name: "supervisorId",
                table: "groups");
        }
    }
}
