using Microsoft.EntityFrameworkCore.Migrations;

namespace Academic_project_manager_WebAPI.Migrations
{
    public partial class coordinatorGroupRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "coordinatorId",
                table: "groups",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_groups_coordinatorId",
                table: "groups",
                column: "coordinatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_groups_Coordinator_coordinatorId",
                table: "groups",
                column: "coordinatorId",
                principalTable: "Coordinator",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_groups_Coordinator_coordinatorId",
                table: "groups");

            migrationBuilder.DropIndex(
                name: "IX_groups_coordinatorId",
                table: "groups");

            migrationBuilder.DropColumn(
                name: "coordinatorId",
                table: "groups");
        }
    }
}
