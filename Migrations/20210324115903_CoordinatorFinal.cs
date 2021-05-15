using Microsoft.EntityFrameworkCore.Migrations;

namespace Academic_project_manager_WebAPI.Migrations
{
    public partial class CoordinatorFinal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coordinator_Supervisors_UserId",
                table: "Coordinator");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Coordinator",
                newName: "SupervisorId");

            migrationBuilder.RenameIndex(
                name: "IX_Coordinator_UserId",
                table: "Coordinator",
                newName: "IX_Coordinator_SupervisorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coordinator_Supervisors_SupervisorId",
                table: "Coordinator",
                column: "SupervisorId",
                principalTable: "Supervisors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coordinator_Supervisors_SupervisorId",
                table: "Coordinator");

            migrationBuilder.RenameColumn(
                name: "SupervisorId",
                table: "Coordinator",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Coordinator_SupervisorId",
                table: "Coordinator",
                newName: "IX_Coordinator_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coordinator_Supervisors_UserId",
                table: "Coordinator",
                column: "UserId",
                principalTable: "Supervisors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
