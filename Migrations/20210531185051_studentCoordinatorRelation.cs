using Microsoft.EntityFrameworkCore.Migrations;

namespace Academic_project_manager_WebAPI.Migrations
{
    public partial class studentCoordinatorRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "coordinatorId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_coordinatorId",
                table: "Students",
                column: "coordinatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Coordinator_coordinatorId",
                table: "Students",
                column: "coordinatorId",
                principalTable: "Coordinator",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Coordinator_coordinatorId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_coordinatorId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "coordinatorId",
                table: "Students");
        }
    }
}
