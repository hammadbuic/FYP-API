using Microsoft.EntityFrameworkCore.Migrations;

namespace Academic_project_manager_WebAPI.Migrations
{
    public partial class updates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_groups_projectRef",
                table: "Project");

            migrationBuilder.RenameColumn(
                name: "projectRef",
                table: "Project",
                newName: "groupId");

            migrationBuilder.RenameIndex(
                name: "IX_Project_projectRef",
                table: "Project",
                newName: "IX_Project_groupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_groups_groupId",
                table: "Project",
                column: "groupId",
                principalTable: "groups",
                principalColumn: "groupId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_groups_groupId",
                table: "Project");

            migrationBuilder.RenameColumn(
                name: "groupId",
                table: "Project",
                newName: "projectRef");

            migrationBuilder.RenameIndex(
                name: "IX_Project_groupId",
                table: "Project",
                newName: "IX_Project_projectRef");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_groups_projectRef",
                table: "Project",
                column: "projectRef",
                principalTable: "groups",
                principalColumn: "groupId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
