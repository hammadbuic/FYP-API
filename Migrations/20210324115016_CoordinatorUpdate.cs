using Microsoft.EntityFrameworkCore.Migrations;

namespace Academic_project_manager_WebAPI.Migrations
{
    public partial class CoordinatorUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coordinator_AspNetUsers_applicationUserId",
                table: "Coordinator");

            migrationBuilder.DropIndex(
                name: "IX_Coordinator_applicationUserId",
                table: "Coordinator");

            migrationBuilder.DropColumn(
                name: "applicationUserId",
                table: "Coordinator");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Coordinator",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coordinator_UserId",
                table: "Coordinator",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coordinator_Supervisors_UserId",
                table: "Coordinator",
                column: "UserId",
                principalTable: "Supervisors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coordinator_Supervisors_UserId",
                table: "Coordinator");

            migrationBuilder.DropIndex(
                name: "IX_Coordinator_UserId",
                table: "Coordinator");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Coordinator",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "applicationUserId",
                table: "Coordinator",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coordinator_applicationUserId",
                table: "Coordinator",
                column: "applicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coordinator_AspNetUsers_applicationUserId",
                table: "Coordinator",
                column: "applicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
