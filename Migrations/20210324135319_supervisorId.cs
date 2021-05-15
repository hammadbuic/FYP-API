using Microsoft.EntityFrameworkCore.Migrations;

namespace Academic_project_manager_WebAPI.Migrations
{
    public partial class supervisorId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coordinator_Supervisors_SupervisorId",
                table: "Coordinator");

            migrationBuilder.DropIndex(
                name: "IX_Coordinator_SupervisorId",
                table: "Coordinator");

            migrationBuilder.RenameColumn(
                name: "SupervisorId",
                table: "Coordinator",
                newName: "supervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_Coordinator_supervisorId",
                table: "Coordinator",
                column: "supervisorId",
                unique: true,
                filter: "[supervisorId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Coordinator_Supervisors_supervisorId",
                table: "Coordinator",
                column: "supervisorId",
                principalTable: "Supervisors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coordinator_Supervisors_supervisorId",
                table: "Coordinator");

            migrationBuilder.DropIndex(
                name: "IX_Coordinator_supervisorId",
                table: "Coordinator");

            migrationBuilder.RenameColumn(
                name: "supervisorId",
                table: "Coordinator",
                newName: "SupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_Coordinator_SupervisorId",
                table: "Coordinator",
                column: "SupervisorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coordinator_Supervisors_SupervisorId",
                table: "Coordinator",
                column: "SupervisorId",
                principalTable: "Supervisors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
