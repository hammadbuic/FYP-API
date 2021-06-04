using Microsoft.EntityFrameworkCore.Migrations;

namespace Academic_project_manager_WebAPI.Migrations
{
    public partial class studentGroup1MRelatin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_groups_GroupId",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "Students",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_groups_GroupId",
                table: "Students",
                column: "GroupId",
                principalTable: "groups",
                principalColumn: "groupId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_groups_GroupId",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_groups_GroupId",
                table: "Students",
                column: "GroupId",
                principalTable: "groups",
                principalColumn: "groupId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
